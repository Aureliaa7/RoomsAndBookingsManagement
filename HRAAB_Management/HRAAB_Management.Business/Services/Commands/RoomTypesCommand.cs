using HRAAB_Management.Business.Abstractions.Interfaces;
using HRAAB_Management.Business.CommandData;
using HRAAB_Management.Business.Entities;
using HRAAB_Management.Business.Enums;
using HRAAB_Management.Business.Extensions;

namespace HRAAB_Management.Business.Services.Commands
{
    public class RoomTypesCommand : BaseCommand, ICommand
    {
        private RoomTypesCommandData data;

        public RoomTypesCommand(ICommandData data, IDataStore dataStore) : base(dataStore)
        {
            if (data is not RoomTypesCommandData)
            {
                throw new ArgumentException("Invalid command data type", nameof(data));
            }
            this.data = (RoomTypesCommandData)data;
        }

        public Task<string> ExecuteAsync()
        {
            Hotel hotel = GetHotel(data.HotelId);

            List<(Room Room, int Size)> availableRoomsWithSizePairs = GetAvailableRoomsWithSize(hotel);
            (List<(Room Room, bool PartiallyFilled)> allocatedRooms, int noRemainingPersons) =
                AllocateRooms(availableRoomsWithSizePairs);

            if (noRemainingPersons > 0)
            {
                return Task.FromResult($"Could not allocate enough rooms for {data.NoPersons} people at '{hotel.Name}'.");
            }

            string result = string.Join(", ",
                allocatedRooms.Select(x =>
                    x.PartiallyFilled
                        ? $"{x.Room.RoomType.ToEnumMemberValue()}!"
                        : x.Room.RoomType.ToEnumMemberValue()
                ));

            return Task.FromResult($"{hotel.Name}: {result}");
        }

        private List<(Room Room, int Size)> GetAvailableRoomsWithSize(Hotel hotel)
        {
            Dictionary<RoomTypeCode, int> roomTypeSizePairs = hotel.RoomTypes
                .ToDictionary(x => x.Code, rt => rt.Size);

            var result = hotel.Rooms
                .Where(x => !IsRoomBooked(x.RoomType, data.Arrival, data.Departure, data.HotelId))
                .Select(x => (x, roomTypeSizePairs[x.RoomType]))
                .OrderByDescending(x => x.Item2)
                .ToList();

            return result;
        }

        private (List<(Room Room, bool PartiallyFilled)> AllocatedRooms, int RemainingPeople)
            AllocateRooms(List<(Room Room, int Size)> rooms)
        {
            var allocatedRoomsStatusPairs = new List<(Room, bool)>();
            int noRemainingPersons = data.NoPersons;

            foreach ((Room room, int size) in rooms)
            {
                if (noRemainingPersons <= 0)
                {
                    break;
                }

                if (size <= noRemainingPersons)
                {
                    allocatedRoomsStatusPairs.Add((room, false));
                    noRemainingPersons -= size;
                }

                else
                {
                    allocatedRoomsStatusPairs.Add((room, true));
                    noRemainingPersons = 0;
                }
            }

            return (allocatedRoomsStatusPairs, noRemainingPersons);
        }
    }
}
