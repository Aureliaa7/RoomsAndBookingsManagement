using HRAAB_Management.Business.Abstractions.Interfaces;
using HRAAB_Management.Business.CommandData;

namespace HRAAB_Management.Business.Services.Commands
{
    public class AvailabilityCommand : ICommand
    {
        private ICommandData data;

        public AvailabilityCommand(ICommandData data)
        {
            if (data is not AvailabilityCommandData)
            {
                throw new ArgumentException("Invalid command data type", nameof(data));
            }
            this.data = data;
        }

        // I think this could implement an abstract class and this method be as a template method
        //
        public Task<string> ExecuteAsync()
        {
            // inject store and check hotel availability
            // return the result as string

            return Task.FromResult("To do");

        }
    }
}
