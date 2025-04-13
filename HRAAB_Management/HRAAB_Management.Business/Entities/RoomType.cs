using HRAAB_Management.Business.Enums;

namespace HRAAB_Management.Business.Entities
{
    public class RoomType
    {
        public required RoomTypeCode Code { get; set; }

        public required int Size { get; set; }

        public required string Description { get; set; }

        public required List<string> Amenities { get; set; }

        public required List<string> Features { get; set; }
    }
}
