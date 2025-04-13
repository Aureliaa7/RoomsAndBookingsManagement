using HRAAB_Management.Business.Enums;

namespace HRAAB_Management.Business.CommandData
{
    public class AvailabilityCommandData : ICommandData
    {
        public string HotelId { get; set; }

        public DateOnly Arrival { get; set; }

        public DateOnly Departure { get; set; }

        public RoomTypeCode RoomType { get; set; }
    }
}
