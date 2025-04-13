using HRAAB_Management.Business.Abstractions.Interfaces;
using HRAAB_Management.Business.Entities;

namespace HRAAB_Management.Business.Services
{
    public class DataStore : IDataStore
    {
        private readonly List<Hotel> hotels = [];

        private readonly List<Booking> bookings = [];

        public void SetHotels(IReadOnlyList<Hotel> hotels)
        {

            this.hotels.Clear();
            this.hotels.AddRange(hotels);
        }

        public IReadOnlyList<Hotel> GetHotels() => hotels;

        public void SetBookings(IReadOnlyList<Booking> bookings)
        {

            this.bookings.Clear();
            this.bookings.AddRange(bookings);
        }

        public IReadOnlyList<Booking> GetBookings() => bookings;
    }
}
