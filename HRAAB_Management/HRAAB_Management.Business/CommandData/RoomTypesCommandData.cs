namespace HRAAB_Management.Business.CommandData
{
    public class RoomTypesCommandData : ICommandData
    {
        public string HotelId { get; set; } = string.Empty;

        public DateOnly Arrival { get; set; }

        public DateOnly Departure { get; set; }

        public int NoPersons { get; set; }
    }
}
