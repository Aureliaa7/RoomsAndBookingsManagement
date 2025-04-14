using HRAAB_Management.Business.Abstractions.Interfaces;
using HRAAB_Management.Business.CommandData;
using HRAAB_Management.Business.Entities;

namespace HRAAB_Management.Business.Services.Commands
{
    public class AvailabilityCommand : BaseCommand, ICommand
    {
        private AvailabilityCommandData data;

        public AvailabilityCommand(ICommandData data, IDataStore dataStore) : base(dataStore)
        {
            if (data is not AvailabilityCommandData)
            {
                throw new ArgumentException("Invalid command data type", nameof(data));
            }
            this.data = (AvailabilityCommandData)data;
        }

        public Task<string> ExecuteAsync()
        {
            Hotel hotel = GetHotel(data.HotelId);

            int overlappingBookings = dataStore.GetBookings()
                .Where(b => b.HotelId == data.HotelId && b.RoomType == data.RoomType)
                .Count(b => IsRoomBooked(data.RoomType, data.Arrival, data.Departure, data.HotelId));

            int totalRooms = hotel.Rooms.Count(r => r.RoomType == data.RoomType);

            return Task.FromResult((totalRooms - overlappingBookings).ToString());
        }
    }
}
