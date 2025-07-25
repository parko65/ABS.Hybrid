using ABSWorker.Contracts;
using Microsoft.AspNetCore.SignalR.Client;

namespace ABSWorker;

public sealed class Worker(ILoggerManager logger, IServiceScopeFactory serviceScopeFactory) : BackgroundService, IAsyncDisposable
{
    private readonly ILoggerManager _logger = logger;
    private HubConnection? _hubConnection;
    private string? userInput;
    private string? messageInput;

    public async override Task StartAsync(CancellationToken cancellationToken)
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl("https://abs.io:8080/chathub")
            .WithAutomaticReconnect()
            .Build();

        await _hubConnection.StartAsync(cancellationToken);

        await base.StartAsync(cancellationToken);
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                userInput = "Worker";
                messageInput = "Hello from the Worker!";

                using IServiceScope scope = serviceScopeFactory.CreateScope();

                IServiceManager service = scope.ServiceProvider.GetRequiredService<IServiceManager>();

                var result = await service.PLCReadService.ReadBasicInfoAsync(7);

                if (_hubConnection is not null)
                {
                    await _hubConnection.SendAsync("SendPLCData", result);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
            // When the stopping token is canceled, for example, a call made from services.msc,
            // we shouldn't exit with a non-zero exit code. In other words, this is expected...
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            // Terminates this process and returns an exit code to the operating system.
            // This is required to avoid the 'BackgroundServiceExceptionBehavior', which
            // performs one of two scenarios:
            // 1. When set to "Ignore": will do nothing at all, errors cause zombie services.
            // 2. When set to "StopHost": will cleanly stop the host, and log errors.
            //
            // In order for the Windows Service Management system to leverage configured
            // recovery options, we need to terminate the process with a non-zero exit code.
            Environment.Exit(1);
        }
    }

    public async override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInfo("Worker stopping");

        await base.StopAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        if (_hubConnection is not null)
        {
            await _hubConnection.DisposeAsync();
        }
    }
}
