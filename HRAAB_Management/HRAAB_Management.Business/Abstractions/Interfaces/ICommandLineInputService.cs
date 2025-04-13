using HRAAB_Management.Business.SettingsModels;

namespace HRAAB_Management.Business.Abstractions.Interfaces
{
    public interface ICommandLineInputService
    {
        CommandLineOptions Parse(string[] args);
    }
}
