using HRAAB_Management.Business.Convertors;
using HRAAB_Management.Business.Enums;
using Newtonsoft.Json;

namespace HRAAB_Management.Business.Entities
{
    public class Booking : IEntity
    {
        public string HotelId { get; set; } = string.Empty;

        [JsonConverter(typeof(DateOnlyNewtonsoftConverter))]
        public DateOnly Arrival { get; set; }

        [JsonConverter(typeof(DateOnlyNewtonsoftConverter))]
        public DateOnly Departure { get; set; }

        public RoomTypeCode RoomType { get; set; }

        public RoomRateType RoomRate { get; set; }
    }
}
