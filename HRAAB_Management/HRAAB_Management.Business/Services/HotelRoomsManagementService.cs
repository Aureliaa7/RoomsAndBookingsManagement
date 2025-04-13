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
            PrintHeader();
            while (true)
            {
                PrintMenu();

                var input = Console.ReadLine()?.Trim();

                switch (input)
                {
                    case "1":
                        graphicUserInterface.PrintMessage("Enter command: ");
                        var userCommand = Console.ReadLine()?.Trim();

                        if (string.IsNullOrEmpty(userCommand))
                        {
                            Environment.Exit(0);
                        }

                        try
                        {
                            ICommand command = commandFactory.GetCommand(userCommand);
                            string result = await command.ExecuteAsync();
                            graphicUserInterface.PrintMessage($"\n** Result: {result}\n");
                        }
                        catch (Exception ex)
                        {
                            graphicUserInterface.PrintMessage($"An error occurred while processing the command: {ex.Message}. Please try again.\n");
                        }
                        break;

                    case "2":
                        Environment.Exit(0);
                        break;

                    case "3":
                        graphicUserInterface.ClearScreen();
                        PrintHeader();
                        break;

                    default:
                        graphicUserInterface.PrintMessage("Invalid option. Please enter a number between 1 and 3.");
                        break;
                }
            }
        }

        private void PrintMenu()
        {
            graphicUserInterface.PrintMessage("\n1.Enter command\n");
            graphicUserInterface.PrintMessage("2.Quit\n");
            graphicUserInterface.PrintMessage("3.Clear the screen\n");
        }

        private void PrintHeader()
        {
            graphicUserInterface.PrintMessage("Welcome to the Hotel Rooms Management System!");
            graphicUserInterface.PrintMessage("Please select an option from the menu below:");
        }
    }
}
