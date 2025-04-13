namespace HRAAB_Management.Business.Exceptions
{
    public class InvalidCommandException : Exception
    {
        public InvalidCommandException(string? message) : base(message)
        {
        }
    }
}
