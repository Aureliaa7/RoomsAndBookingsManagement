using HRAAB_Management.Business.Abstractions;
using HRAAB_Management.Business.CommandData;
using HRAAB_Management.Business.SettingsModels;
using Microsoft.Extensions.Options;

namespace HRAAB_Management.Business.Services.Parsers
{
    public class RoomTypesCommandParser : BaseCommandParser<RoomTypesCommandData>
    {
        private const int HotelIdIndex = 0;
        private const int DateIndex = 1;
        private const int NoPersonsIndex = 2;

        public RoomTypesCommandParser(IOptions<RoomTypesCommandSettings> settings)
           : base(settings.Value.CommandName, settings.Value.NoExpectedArguments, settings.Value.DateTimeFormat) { }

        protected override ICommandData CreateCommandData(string sanitizedInput)
        {
            string[] parts = GetCommandParts(sanitizedInput);

            (DateOnly arrival, DateOnly departure) = GetDateRange(parts[DateIndex].Trim());

            var data = new RoomTypesCommandData
            {
                HotelId = parts[HotelIdIndex].Trim(),
                Arrival = arrival,
                Departure = departure,
                NoPersons = int.Parse(parts[NoPersonsIndex].Trim()),
            };

            return data;
        }
    }
}
