using Microsoft.AspNetCore.Mvc;
using gamebox.Server.HubHelpers;

namespace gamebox.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameCoordinatorController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;

        public GameCoordinatorController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [HttpGet]
        [Route("gamecode")]
        public string GameCode()
        {
            var gameCode = string.Empty;
            do
            {
                gameCode = GenerateRandomGameCode();
            } while (_gameRepository.DoesGameExist(gameCode)); // Avoid duplicate game codes

            return gameCode;
        }

        private string GenerateRandomGameCode()
        {
            var gameCode = string.Empty;
            var random = new Random();
            gameCode += (char) random.Next(65, 90); // A-Z
            gameCode += (char) random.Next(65, 90);
            gameCode += (char) random.Next(65, 90);
            gameCode += (char) random.Next(65, 90);

            return gameCode;
        }
    }
}