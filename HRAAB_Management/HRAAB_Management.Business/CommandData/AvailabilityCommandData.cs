using HRAAB_Management.Business.Enums;

namespace HRAAB_Management.Business.CommandData
{
    public class AvailabilityCommandData : ICommandData
    {
        public string HotelId { get; set; }

        public DateTime Arrival { get; set; }

        public DateTime? Departure { get; set; }

        public RoomTypeCode RoomType { get; set; }
    }
}
