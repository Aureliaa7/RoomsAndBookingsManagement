using HRAAB_Management.Business.Abstractions;
using HRAAB_Management.Business.CommandData;
using HRAAB_Management.Business.SettingsModels;
using Microsoft.Extensions.Options;

namespace HRAAB_Management.Business.Services.Parsers
{
    public class RoomTypesCommandParser : BaseCommandParser<RoomTypesCommandData>
    {
        public RoomTypesCommandParser(IOptions<RoomTypesCommandSettings> settings)
           : base(settings.Value.CommandName, settings.Value.NoExpectedArguments) { }

        protected override ICommandData CreateCommandData(string sanitizedInput)
        {
            throw new NotImplementedException();
        }
    }
}
