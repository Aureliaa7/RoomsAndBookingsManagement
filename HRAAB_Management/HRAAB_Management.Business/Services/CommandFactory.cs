using HRAAB_Management.Business.Abstractions;
using HRAAB_Management.Business.Abstractions.Interfaces;
using HRAAB_Management.Business.CommandData;
using HRAAB_Management.Business.Enums;
using HRAAB_Management.Business.Exceptions;
using HRAAB_Management.Business.Services.Commands;

namespace HRAAB_Management.Business.Services
{
    public class CommandFactory : ICommandFactory
    {
        private readonly BaseCommandParser<AvailabilityCommandData> availabilityCommandParser;
        private readonly BaseCommandParser<RoomTypesCommandData> roomTypeCommandParser;
        private readonly IDataStore dataStore;

        public CommandFactory(
            BaseCommandParser<AvailabilityCommandData> availabilityCommandParser,
            BaseCommandParser<RoomTypesCommandData> roomTypeCommandParser,
            IDataStore dataStore)
        {
            this.availabilityCommandParser = availabilityCommandParser;
            this.roomTypeCommandParser = roomTypeCommandParser;
            this.dataStore = dataStore;
        }

        public ICommand GetCommand(string input)
        {
            if (input.StartsWith(CommandType.Availability.ToString()))
            {
                ICommandData data = availabilityCommandParser.Parse(input);
                return new AvailabilityCommand(data, dataStore);
            }
            else if (input.StartsWith(CommandType.RoomTypes.ToString()))
            {
                ICommandData data = roomTypeCommandParser.Parse(input);
                return new RoomTypesCommand(data, dataStore);
            }

            throw new InvalidCommandException($"Invalid command: {nameof(input)}");
        }
    }
}
