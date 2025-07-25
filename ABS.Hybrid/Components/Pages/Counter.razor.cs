using ABS.Hybrid.Components.Layout;
using ABS.Hybrid.ServiceTimers;
using Microsoft.FluentUI.AspNetCore.Components;

namespace ABS.Hybrid.Components.Pages;
public partial class Counter : IAsyncDisposable
{
    private readonly IMessageService _messageService;    

    public BackgroundTask Task { get; set; } = default!;

    private int currentCount = 0;

    public Counter(IMessageService messageService, BackgroundTask backgroundTask)
    {
        _messageService = messageService;
        Task = backgroundTask;
    }

    private void IncrementCount()
    {
        currentCount++;        
    }

    private async Task AddInTopBar()
    {
        var intent = MessageIntent.Error;

        await _messageService.ShowMessageBarAsync(options =>
        {
            options.Title = "Test";
            options.Intent = intent;
            options.Section = MainLayout.MESSAGES_TOP;
            options.Timeout = 6000;
        });
    }

    protected override void OnInitialized()
    {        
        Task.Start();
    }

    public async ValueTask DisposeAsync()
    {
        if (Task is not null)
        {
            await Task.StopAsync();
        }
    }
}
