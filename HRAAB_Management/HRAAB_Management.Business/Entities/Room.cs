using HRAAB_Management.Business.Enums;

namespace HRAAB_Management.Business.Entities
{
    public class Room
    {
        public required RoomTypeCode RoomType { get; set; }

        public required int RoomId { get; set; }
    }
}
