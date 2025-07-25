using ABS.Hybrid.Extensions;
using ABS.Hybrid.ServiceTimers;
using libplctag.NativeImport;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;
using NLog;
using Service.Contracts;

namespace ABS.Hybrid;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddFluentUIComponents();

        builder.SetupConfiguration();

        builder.Services.ConfigureLoggerService();
        builder.Services.ConfigureRepositoryManager();
        builder.Services.ConfigureServiceManager();
        builder.Services.ConfigureSqlContext(builder.Configuration);

        builder.Services.AddAutoMapper(typeof(MauiProgram));

        builder.Services.AddSingleton<BackgroundTask>(sp => new BackgroundTask(TimeSpan.FromMilliseconds(1000), sp.GetRequiredService<IServiceManager>()));        

        plctag.ForceExtractLibrary = false; // Do not extract the library, use the one in the native import

#if DEBUG
        var env = "Development";
#else
        var env = "Production";
#endif

        builder.Services.AddSingleton<IHostEnvironment>(sp =>
        new HostingEnvironment
        {
            EnvironmentName = env, // Or get from config
            ApplicationName = AppDomain.CurrentDomain.FriendlyName
        });

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
