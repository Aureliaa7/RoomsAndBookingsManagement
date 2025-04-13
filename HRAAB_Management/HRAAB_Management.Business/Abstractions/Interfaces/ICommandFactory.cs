namespace HRAAB_Management.Business.Abstractions.Interfaces
{
    public interface ICommandFactory
    {
        /// <summary>
        /// Creates commands based on the input string.
        /// </summary>
        ICommand GetCommand(string input);
    }
}
