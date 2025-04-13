using HRAAB_Management.Business.Abstractions.Interfaces;
using HRAAB_Management.Business.Entities;
using HRAAB_Management.Business.SettingsModels;

namespace HRAAB_Management.Business.Services
{
    public class HotelRoomsManagementService : IHotelRoomsManagementService
    {
        private readonly ICommandFactory commandFactory;
        private readonly IGraphicUserInterface graphicUserInterface;
        private readonly ICommandLineInputService commandLineInputService;
        private readonly IDataLoader dataLoader;
        private readonly IDataStore dataStore;

        public HotelRoomsManagementService(
            ICommandFactory commandFactory,
            IGraphicUserInterface graphicUserInterface,
            ICommandLineInputService commandLineInputService,
            IDataLoader dataLoader,
            IDataStore dataStore)
        {
            this.commandFactory = commandFactory;
            this.graphicUserInterface = graphicUserInterface;
            this.commandLineInputService = commandLineInputService;
            this.dataLoader = dataLoader;
            this.dataStore = dataStore;
        }

        public async Task InitializeAsync(string[] args)
        {
            CommandLineOptions commandLineOptions = commandLineInputService.Parse(args);
            IReadOnlyList<Hotel> hotelsData = await dataLoader.LoadAsync<Hotel>(commandLineOptions.HotelsFilePath);
            IReadOnlyList<Booking> bookingsData = await dataLoader.LoadAsync<Booking>(commandLineOptions.BookingsFilePath);
            dataStore.SetHotels(hotelsData);
            dataStore.SetBookings(bookingsData);
        }

        public async Task RunAsync()
        {
            graphicUserInterface.PrintMessage("Welcome to the Hotel Rooms Management System!\n");
            while (true)
            {
                // todo: create a menu and allow the user to clear the screen
                graphicUserInterface.PrintMessage("Enter command (or 'exit' to quit): ");
                string? input = graphicUserInterface.ReadString();

                if (string.IsNullOrEmpty(input))
                {
                    Environment.Exit(0);
                }

                try
                {
                    ICommand command = commandFactory.GetCommand(input);
                    string result = await command.ExecuteAsync();
                    graphicUserInterface.PrintMessage($"** Result: {result}\n");
                }
                catch (Exception ex)
                {
                    graphicUserInterface.PrintMessage($"An error occurred while processing the command: {ex.Message}. Please try again.\n");
                }
            }
        }
    }
}
