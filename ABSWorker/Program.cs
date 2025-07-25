using ABSWorker;
using ABSWorker.Configuration;
using ABSWorker.Extensions;
using NLog;

var builder = Host.CreateApplicationBuilder(args);

LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "ABS Worker Service";
});

builder.Services.AddHostedService<Worker>();

builder.Services.ConfigureLoggerService();
builder.Services.ConfigureServiceManager();

builder.Services.Configure<PlcSettings>(builder.Configuration.GetSection("PlcSettings"));

var host = builder.Build();
host.Run();
