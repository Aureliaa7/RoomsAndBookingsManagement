namespace HRAAB_Management.Business.Abstractions.Interfaces
{
    public interface IHotelRoomsManagementService
    {
        /// <summary>
        /// Initializes the system by loading data from files and setting up the necessary components.
        /// </summary>
        /// <returns></returns>
        Task InitializeAsync(string[] args);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task RunAsync();
    }
}
