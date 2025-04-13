using HRAAB_Management.Business.Abstractions.Interfaces;

namespace HRAAB_Management.Business.Services
{
    public class AppRunner : IAppRunner
    {
        private readonly IHotelRoomsManagementService hotelRoomsManagementService;

        public AppRunner(IHotelRoomsManagementService hotelRoomsManagementService)
        {
            this.hotelRoomsManagementService = hotelRoomsManagementService;
        }

        public async Task RunAsync(string[] args)
        {
            await hotelRoomsManagementService.InitializeAsync(args);
            await hotelRoomsManagementService.RunAsync();
        }
    }
}
