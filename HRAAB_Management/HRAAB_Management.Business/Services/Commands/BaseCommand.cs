using HRAAB_Management.Business.Abstractions.Interfaces;
using HRAAB_Management.Business.Entities;
using HRAAB_Management.Business.Enums;
using HRAAB_Management.Business.Exceptions;

namespace HRAAB_Management.Business.Services.Commands
{
    public class BaseCommand
    {
        protected readonly IDataStore dataStore;

        protected BaseCommand(IDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        protected Hotel GetHotel(string hotelId)
        {
            return dataStore.GetHotels().FirstOrDefault(h => h.Id == hotelId)
                ?? throw new EntityNotFoundException($"Hotel with ID {hotelId} was not found!");
        }

        protected List<Room> GetAvailableRooms(Hotel hotel, DateOnly arrival, DateOnly departure)
        {
            return hotel.Rooms
                .Where(room => !IsRoomBooked(room.RoomType, arrival, departure, hotel.Id))
                .ToList();
        }

        protected bool IsRoomBooked(RoomTypeCode roomType, DateOnly arrival, DateOnly departure, string hotelId)
        {
            return dataStore
                .GetBookings()
                .Any(b => b.HotelId == hotelId &&
                          b.RoomType == roomType &&
                          !(b.Departure <= arrival || b.Arrival >= departure));
        }
    }
}
