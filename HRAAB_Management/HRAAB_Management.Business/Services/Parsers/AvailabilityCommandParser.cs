using HRAAB_Management.Business.Abstractions;
using HRAAB_Management.Business.CommandData;
using HRAAB_Management.Business.Enums;
using HRAAB_Management.Business.Helpers;
using HRAAB_Management.Business.SettingsModels;
using Microsoft.Extensions.Options;

namespace HRAAB_Management.Business.Services.Parsers
{
    public class AvailabilityCommandParser : BaseCommandParser<AvailabilityCommandData>
    {
        private const int HotelIdIndex = 0;
        private const int DateIndex = 1;
        private const int RoomTypeCodeIndex = 2;

        public AvailabilityCommandParser(IOptions<AvailabilityCommandSettings> settings)
            : base(settings.Value.CommandName, settings.Value.NoExpectedArguments, settings.Value.DateTimeFormat)
        {
        }

        protected override ICommandData CreateCommandData(string sanitizedInput)
        {
            string[] parts = GetCommandParts(sanitizedInput);

            (DateOnly arrival, DateOnly departure) = GetDateRange(parts[DateIndex].Trim());

            AvailabilityCommandData data = new AvailabilityCommandData
            {
                HotelId = parts[HotelIdIndex].Trim(),
                Arrival = arrival,
                Departure = departure,
                RoomType = EnumHelper.ParseFromEnumMember<RoomTypeCode>(parts[RoomTypeCodeIndex].Trim()),
            };

            return data;
        }
    }
}
