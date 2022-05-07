namespace gamebox.Server.HubHelpers
{
    public interface IGameRepository
    {
        bool IsUserInGame(string connectionId, string gameCode);
        void AddUserToGame(string connectionId, string gameCode);
        string? GetGameByUser(string connectionId);
        bool DoesGameExist(string gameCode);
    }
}
