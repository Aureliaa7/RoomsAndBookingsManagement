using HRAAB_Management.Business.Abstractions;
using HRAAB_Management.Business.Abstractions.Interfaces;
using HRAAB_Management.Business.CommandData;
using HRAAB_Management.Business.GraphicUserInterface;
using HRAAB_Management.Business.Services;
using HRAAB_Management.Business.Services.Parsers;
using HRAAB_Management.Business.SettingsModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


IHostBuilder builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.Sources.Clear();

        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        config.AddEnvironmentVariables();
        config.AddCommandLine(args);
    })
    .ConfigureServices((context, services) =>
    {
        ConfigureServices(context, services);
    });

IHost host = builder.Build();
await HandleCommandsAsync();

async Task HandleCommandsAsync()
{
    IAppRunner appRunner = host.Services.GetRequiredService<IAppRunner>();
    await appRunner.RunAsync(args);
}

void ConfigureServices(HostBuilderContext context, IServiceCollection services)
{
    services.Configure<AvailabilityCommandSettings>(context.Configuration.GetSection(AvailabilityCommandSettings.SettingsKey));
    services.Configure<RoomTypesCommandSettings>(context.Configuration.GetSection(RoomTypesCommandSettings.SettingsKey));
    services.AddSingleton<IHotelRoomsManagementService, HotelRoomsManagementService>();
    services.AddSingleton<ICommandFactory, CommandFactory>();
    services.AddSingleton<BaseCommandParser<AvailabilityCommandData>, AvailabilityCommandParser>();
    services.AddSingleton<BaseCommandParser<RoomTypesCommandData>, RoomTypesCommandParser>();
    services.AddSingleton<IDataStore, DataStore>();
    services.AddSingleton<IGraphicUserInterface, ConsoleUserInterface>();
    services.AddSingleton<ICommandLineInputService, CommandLineInputService>();
    services.AddSingleton<IDataLoader, JsonLoader>();
    services.AddSingleton<IAppRunner, AppRunner>();
}