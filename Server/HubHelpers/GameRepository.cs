namespace gamebox.Server.HubHelpers
{
    public class GameRepository : IGameRepository
    {
        // TODO replace this in memory store with Redis with a TTL
        private readonly Dictionary<string, string> _userInformation = new();

        public void AddUserToGame(string user, string gameCode) => _userInformation.Add(user, gameCode);

        public bool IsUserInGame(string user, string gameCode)=>  _userInformation.ContainsKey(user);
        public string? GetGameByUser(string connectionId)
        {
            return _userInformation.TryGetValue(connectionId, out var game) ? game : null;
        }
        public bool DoesGameExist(string gameCode) => _userInformation.ContainsValue(gameCode);
    }
}
