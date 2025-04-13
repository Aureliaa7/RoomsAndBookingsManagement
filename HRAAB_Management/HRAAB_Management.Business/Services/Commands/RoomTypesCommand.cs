using HRAAB_Management.Business.Abstractions.Interfaces;
using HRAAB_Management.Business.CommandData;
using HRAAB_Management.Business.Entities;
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
            List<Room> allAvailableRooms = GetAvailableRooms(hotel, data.Arrival, data.Departure);
            List<string> allocatedRoomsCodes = [];
            int noRemainingPersons = data.NoPersons;

            foreach (RoomType roomType in hotel.RoomTypes.OrderByDescending(rt => rt.Size))
            {
                int noRoomsAvailable = allAvailableRooms.Count(r => r.RoomType == roomType.Code);

                // If we still need rooms of this type
                if (noRoomsAvailable > 0)
                {
                    int noRoomsNeeded = Math.Min(noRoomsAvailable, noRemainingPersons / roomType.Size);
                    if (noRemainingPersons % roomType.Size != 0)
                    {
                        // If there’s a partial room needed
                        noRoomsNeeded++;
                    }

                    for (int i = 0; i < noRoomsNeeded; i++)
                    {
                        // partial room
                        if (noRemainingPersons % roomType.Size != 0 && i == noRoomsNeeded - 1)
                        {
                            allocatedRoomsCodes.Add($"{roomType.Code.ToEnumMemberValue()}!");
                        }
                        else
                        {
                            allocatedRoomsCodes.Add(roomType.Code.ToEnumMemberValue());
                        }
                    }

                    noRemainingPersons -= noRoomsNeeded * roomType.Size;
                }

                if (noRemainingPersons <= 0)
                {
                    break;
                }
            }

            if (noRemainingPersons > 0)
            {
                return Task.FromResult($"Could not allocate enough rooms for {data.NoPersons} people at hotel '{hotel.Name}'.");
            }

            return Task.FromResult($"{hotel.Name}: {string.Join(",", allocatedRoomsCodes)}");
        }
    }
}
