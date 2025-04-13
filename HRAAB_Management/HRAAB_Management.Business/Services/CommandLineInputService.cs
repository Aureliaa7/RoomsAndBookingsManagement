using HRAAB_Management.Business.Abstractions.Interfaces;
using HRAAB_Management.Business.Exceptions;
using HRAAB_Management.Business.SettingsModels;

namespace HRAAB_Management.Business.Services
{
    public class CommandLineInputService : ICommandLineInputService
    {
        public CommandLineOptions Parse(string[] args)
        {
            CommandLineOptions options = new CommandLineOptions();

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case Constants.CommandLineOptions.HotelsFlag:
                        if (i + 1 < args.Length)
                        {
                            options.HotelsFilePath = args[++i];
                        }
                        break;

                    case Constants.CommandLineOptions.BookingsFlag:
                        if (i + 1 < args.Length)
                        {
                            options.BookingsFilePath = args[++i];
                        }
                        break;

                    default:
                        throw new CommandLineOptionException($"Unknown command line option: {args[i]}");
                }
            }

            ValidateCommandLineOptions(options);

            return options;
        }

        private static void ValidateCommandLineOptions(CommandLineOptions options)
        {
            if (string.IsNullOrEmpty(options.HotelsFilePath))
            {
                throw new CommandLineOptionException("Hotels file path is required.");
            }
            if (string.IsNullOrEmpty(options.BookingsFilePath))
            {
                throw new CommandLineOptionException("Bookings file path is required.");
            }
        }
    }
}