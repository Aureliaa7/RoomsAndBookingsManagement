namespace HRAAB_Management.Business.Exceptions
{
    public class NotEnoughRoomsException : Exception
    {
        public NotEnoughRoomsException(string? message) : base(message)
        {
        }
    }
}
