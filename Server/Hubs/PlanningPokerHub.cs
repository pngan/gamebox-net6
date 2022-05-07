using Microsoft.AspNetCore.SignalR;
namespace gamebox.Server.Hubs;

public class PlanningPokerHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
