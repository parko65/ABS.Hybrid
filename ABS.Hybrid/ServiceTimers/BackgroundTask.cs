using System.Diagnostics;
using Service.Contracts;

namespace ABS.Hybrid.ServiceTimers;
public class BackgroundTask
{
    private Task? _timerTask;
    private readonly PeriodicTimer _timer;
    private readonly CancellationTokenSource _cts = new();
    private readonly IServiceManager _service;

    public BackgroundTask(TimeSpan interval, IServiceManager service)
    {
        _timer = new PeriodicTimer(interval);
        _service = service;
    }

    public void Start()
    {
        _timerTask = DoWorkAsync();
    }

    private async Task DoWorkAsync()
    {
        try
        {
            while (await _timer.WaitForNextTickAsync(_cts.Token))
            {
                var result = await _service.PlcReadService.ReadBasicInfoAsync(2);
                if (result is not null && result.Length == 2)
                {
                    Debug.WriteLine($"Basic Info: {result[0]}, {result[1]}");
                }
                else
                {
                    Debug.WriteLine("Failed to read basic info from PLC.");
                }
            }
        }
        catch (OperationCanceledException)
        {
            Debug.WriteLine("Timer stopped.");
        }
    }

    public async Task StopAsync()
    {
        if (_timerTask is null)
        {
            return;
        }

        _cts.Cancel();

        try
        {
            await _timerTask;
        }
        catch (OperationCanceledException)
        {
            // Expected when cancelling
            Debug.WriteLine("Timer stopped.");
        }

        _cts.Dispose();

        _timer.Dispose();

        Debug.WriteLine("Task was cancelled.");
    }    
}
