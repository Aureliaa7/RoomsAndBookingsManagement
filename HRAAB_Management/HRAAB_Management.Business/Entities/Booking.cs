using HRAAB_Management.Business.Enums;

namespace HRAAB_Management.Business.Entities
{
    public class Booking : IEntity
    {
        public int HotelId { get; set; }

        public DateTime Arrival { get; set; }

        public DateTime Departure { get; set; }

        public RoomTypeCode RoomType { get; set; }

        public RoomRateType RoomRate { get; set; }
    }
}
