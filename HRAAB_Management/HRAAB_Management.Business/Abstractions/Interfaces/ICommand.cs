namespace HRAAB_Management.Business.Abstractions.Interfaces
{
    public interface ICommand
    {
        /// <summary>
        /// Executes the command with the provided data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<string> ExecuteAsync();
    }
}
