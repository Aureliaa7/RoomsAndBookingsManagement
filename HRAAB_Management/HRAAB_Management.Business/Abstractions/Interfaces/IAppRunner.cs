namespace HRAAB_Management.Business.Abstractions.Interfaces
{
    public interface IAppRunner
    {
        /// <summary>
        /// Initializes the system by loading data from files and setting up the necessary components.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        Task RunAsync(string[] args);
    }
}
