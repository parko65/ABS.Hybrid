using Microsoft.AspNetCore.SignalR;
using PLCEntities;

namespace ABSHub.Hubs;

public class ChatHub : Hub
{    public async Task SendMessage(string user, string message)
    {
        // Send the message to all connected clients
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task SendPLCData(TagModel tag)
    {
        // Send the message to all connected clients
        await Clients.All.SendAsync("ReceivePLCData", tag);
    }

    public async Task SendPLCActive(bool isActive)
    {
        // Send the active status to all connected clients
        await Clients.All.SendAsync("ReceivePLCActive", isActive);
    }
}
