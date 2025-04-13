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
            : base(settings.Value.CommandName, settings.Value.NoExpectedArguments) { }

        protected override ICommandData CreateCommandData(string sanitizedInput)
        {
            var parts = sanitizedInput.Split([','], StringSplitOptions.RemoveEmptyEntries);
            var dateParts = parts[DateIndex].Split(['-'], StringSplitOptions.RemoveEmptyEntries);
            if (dateParts.Length > 2)
            {
                throw new ArgumentException("Invalid date format", nameof(sanitizedInput));
            }

            DateTime arrival;
            DateTime departure;
            if (dateParts.Length == 1)
            {
                // TODO: extract format date to appsettings
                arrival = DateTime.ParseExact(dateParts[0].Trim(), "yyyyMMdd", null);
                departure = arrival.AddDays(1);
            }
            else if (dateParts.Length == 2)
            {
                arrival = DateTime.ParseExact(dateParts[0].Trim(), "yyyyMMdd", null);
                departure = DateTime.ParseExact(dateParts[1].Trim(), "yyyyMMdd", null);
            }
            else
            {
                throw new ArgumentException("Invalid date format", nameof(sanitizedInput));
            }

            var data = new AvailabilityCommandData
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
