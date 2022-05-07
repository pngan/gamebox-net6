using gamebox.Server.HubHelpers;
using Microsoft.AspNetCore.SignalR;

namespace gamebox.Server.Hubs;

public class PlanningPokerHub : Hub
{
    private readonly IGameRepository _gameRepository;

    public PlanningPokerHub(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task SendMessage(string user, string message, string gameCode)
    {
        var isUserInGame = _gameRepository.IsUserInGame(Context.ConnectionId, gameCode);
        if (!isUserInGame)
        {
            _gameRepository.AddUserToGame(Context.ConnectionId, gameCode);
            await Groups.AddToGroupAsync(Context.ConnectionId, gameCode);
        }

        await Clients.Group(gameCode).SendAsync("ReceiveMessage", user, message);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var game = _gameRepository.GetGameByUser(Context.ConnectionId);
        if (game is null) return;
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, game);
    }
}
