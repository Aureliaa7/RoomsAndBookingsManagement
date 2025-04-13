namespace HRAAB_Management.Business.CommandData
{
    /// <summary>
    /// Interface for commands data.
    /// </summary>
    public interface ICommandData
    {
        public string HotelId { get; set; }

        public DateOnly Arrival { get; set; }

        public DateOnly Departure { get; set; }

    }
}
