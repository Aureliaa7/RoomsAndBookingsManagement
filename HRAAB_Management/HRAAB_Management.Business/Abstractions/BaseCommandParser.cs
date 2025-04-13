using HRAAB_Management.Business.CommandData;
using System.Text;

namespace HRAAB_Management.Business.Abstractions
{
    public abstract class BaseCommandParser<T> where T : ICommandData, new()
    {
        protected string commandType;
        protected int expectedArgumentsCount;

        protected BaseCommandParser(string commandType, int expectedArgumentsCount)
        {
            this.commandType = commandType;
            this.expectedArgumentsCount = expectedArgumentsCount;
        }
        protected abstract ICommandData CreateCommandData(string sanitizedInput);

        public ICommandData Parse(string input)
        {
            string sanitizedInput = SanitizeInput(input);
            ICommandData data = CreateCommandData(sanitizedInput);
            return data;
        }

        private string SanitizeInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("Command cannot be null or empty!", nameof(input));
            }

            StringBuilder sanitizedInput = new(input.Trim());
            sanitizedInput = sanitizedInput.Replace(commandType, string.Empty);
            sanitizedInput = sanitizedInput.Replace(Constants.OpeningParanthesis, string.Empty);
            sanitizedInput = sanitizedInput.Replace(Constants.ClosingParanthesis, string.Empty);

            string[] parts = input.Split([','], StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < expectedArgumentsCount)
            {
                throw new ArgumentException("Invalid command format", nameof(input));
            }
            return sanitizedInput.ToString();
        }
    }
}
