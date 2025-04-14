using HRAAB_Management.Business.CommandData;
using System.Text;

namespace HRAAB_Management.Business.Abstractions
{
    public abstract class BaseCommandParser<T> where T : ICommandData, new()
    {
        private readonly string commandType;
        private readonly int expectedArgumentsCount;
        private readonly string dateTimeFormat;

        private const int ArrivalDateIndex = 0;
        private const int DepartureDateIndex = 1;

        protected BaseCommandParser(string commandType, int expectedArgumentsCount, string dateTimeFormat)
        {
            this.commandType = commandType;
            this.expectedArgumentsCount = expectedArgumentsCount;
            this.dateTimeFormat = dateTimeFormat;
        }

        protected abstract ICommandData CreateCommandData(string sanitizedInput);

        protected string[] GetCommandParts(string sanitizedInput)
        {
            return sanitizedInput.Split([','], StringSplitOptions.RemoveEmptyEntries);
        }

        protected (DateOnly arrival, DateOnly departure) GetDateRange(string date)
        {
            string[] dateParts = date.Split(['-'], StringSplitOptions.RemoveEmptyEntries);
            if (dateParts.Length > 2)
            {
                throw new ArgumentException("Invalid date format", nameof(date));
            }

            DateOnly arrival;
            DateOnly departure;
            if (dateParts.Length == 1)
            {
                arrival = GetDate(dateParts[ArrivalDateIndex]);
                departure = arrival.AddDays(1);
            }
            else if (dateParts.Length == 2)
            {
                arrival = GetDate(dateParts[ArrivalDateIndex]);
                departure = GetDate(dateParts[DepartureDateIndex]);
            }
            else
            {
                throw new ArgumentException("Invalid date format", nameof(date));
            }

            return new(arrival, departure);
        }

        public ICommandData Parse(string input)
        {
            string sanitizedInput = SanitizeInput(input);
            ICommandData data = CreateCommandData(sanitizedInput);
            return data;
        }

        private DateOnly GetDate(string date)
        {
            return DateOnly.ParseExact(date.Trim(), dateTimeFormat, null);
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
            if (parts.Length != expectedArgumentsCount)
            {
                throw new ArgumentException("Invalid command format", nameof(input));
            }
            return sanitizedInput.ToString();
        }
    }
}
