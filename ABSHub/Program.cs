using System.Net;
using ABSHub;
using ABSHub.Hubs;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Hosting.WindowsServices;

var webOptions = new WebApplicationOptions
{
    Args = args,
    ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default
};

var builder = WebApplication.CreateBuilder(webOptions);

HostConfig.CertPath = builder.Configuration["CertPath"]!;
HostConfig.CertPassword = builder.Configuration["CertPassword"]!;

// Configure Kestrel server
builder.WebHost.ConfigureKestrel((context, options) =>
{
    var host = Dns.GetHostEntry("abs.io");

    options.Listen(host.AddressList[0], 8080, listenOptions =>
    {
        listenOptions.UseHttps(HostConfig.CertPath, HostConfig.CertPassword);
    });
});

// Add services to the container.
builder.Services.AddSignalR();

builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "ABS Hub Service";
});

builder.Services.AddResponseCompression(options =>
{
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        ["application/octet-stream"]);
});

var app = builder.Build();

app.UseResponseCompression();

app.UseHttpsRedirection();

app.MapHub<ChatHub>("/chathub");

app.Run();
