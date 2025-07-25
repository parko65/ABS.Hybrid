using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository;
using Service;
using Service.Contracts;
using System.Reflection;

namespace ABS.Hybrid.Extensions;
public static class ServiceExtensions
{
    public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();

    public static void SetupConfiguration(this MauiAppBuilder builder)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using Stream? stream = assembly.GetManifestResourceStream("ABS.Hybrid.appsettings.json")
            ?? throw new FileNotFoundException("appsettings.json not found in the assembly manifest resources.");
        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();        

        builder.Configuration.AddConfiguration(config);
    }

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IServiceManager, ServiceManager>();

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RepositoryContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }

    //public static void ConfigureSqliteContext(this IServiceCollection services, IConfiguration configuration)
    //{
    //    services.AddDbContext<RepositoryContext>(opts =>
    //        opts.UseSqlite(configuration.GetConnectionString("SqliteConnection")));
    //}
}