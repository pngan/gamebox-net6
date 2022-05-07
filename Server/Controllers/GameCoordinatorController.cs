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
            } while (_gameRepository.NumberOfPlayers(gameCode) > 0); // Avoid duplicate game codes

            return gameCode;
        }


        [HttpGet]
        [Route("numberofplayers/{gamecode}")]
        public int NumberOfPlayers(string gamecode)
        {
            return _gameRepository.NumberOfPlayers(gamecode);
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