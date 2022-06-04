using gamebox.Server.HubHelpers;
using Microsoft.AspNetCore.SignalR;

namespace gamebox.Server.Hubs;

public class GameHub : Hub
{
    public async Task BroadcastGameInfo(string gameCode, string gameInfo)
    {
        await Clients.Group(gameCode).SendAsync("BroadcastGameInfo", gameInfo);
    }

    public async Task JoinGame(string gameCode)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, gameCode);
    }
}
