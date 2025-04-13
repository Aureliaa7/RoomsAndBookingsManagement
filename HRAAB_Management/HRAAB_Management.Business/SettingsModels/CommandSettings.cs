namespace HRAAB_Management.Business.SettingsModels
{
    public class CommandSettings
    {
        public string CommandName { get; set; } = string.Empty;

        public int NoExpectedArguments { get; set; } = 0;

        public string DateTimeFormat { get; set; } = string.Empty;
    }
}
