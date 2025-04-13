using HRAAB_Management.Business.Entities;

namespace HRAAB_Management.Business.Abstractions.Interfaces
{
    public interface IDataStore
    {
        void SetHotels(IReadOnlyList<Hotel> hotels);

        IReadOnlyList<Hotel> GetHotels();

        void SetBookings(IReadOnlyList<Booking> bookings);

        IReadOnlyList<Booking> GetBookings();
    }
}
