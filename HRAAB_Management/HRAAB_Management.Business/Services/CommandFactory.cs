using HRAAB_Management.Business.Abstractions;
using HRAAB_Management.Business.Abstractions.Interfaces;
using HRAAB_Management.Business.CommandData;
using HRAAB_Management.Business.Enums;
using HRAAB_Management.Business.Services.Commands;

namespace HRAAB_Management.Business.Services
{
    public class CommandFactory : ICommandFactory
    {
        private readonly BaseCommandParser<AvailabilityCommandData> availabilityCommandParser;
        private readonly BaseCommandParser<RoomTypesCommandData> roomTypeCommandParser;

        public CommandFactory(
            BaseCommandParser<AvailabilityCommandData> availabilityCommandParser,
            BaseCommandParser<RoomTypesCommandData> roomTypeCommandParser)
        {
            this.availabilityCommandParser = availabilityCommandParser;
            this.roomTypeCommandParser = roomTypeCommandParser;
        }

        public ICommand GetCommand(string input)
        {
            if (input.StartsWith(CommandType.Availability.ToString()))
            {
                ICommandData command = availabilityCommandParser.Parse(input);
                return new AvailabilityCommand(command);
            }
            else if (input.StartsWith(CommandType.RoomTypes.ToString()))
            {
                ICommandData data = roomTypeCommandParser.Parse(input);
                return new RoomTypesCommand();
            }

            //TODO: create custom exception
            throw new ArgumentException("Invalid command", nameof(input));
        }
    }
}
