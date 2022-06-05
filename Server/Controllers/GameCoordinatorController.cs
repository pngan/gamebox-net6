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
            } while (!string.IsNullOrWhiteSpace(_gameRepository.GetGameInfo(gameCode))); // Avoid duplicate game codes

            return gameCode;
        }


        [HttpGet]
        [Route("{gamecode}")]
        public string GetGameInfo(string gamecode)
        {
            var gameInfo = _gameRepository.GetGameInfo(gamecode);
            return gameInfo;
        }

        [HttpPost]
        [Route("{gamecode}")]
        public void AddOrUpdateGameInfo(string gamecode)
        {
            var value = Request.BodyReader.ReadAsync().Result;

            var buffer = value.Buffer;
            var gameInfo =System.Text.Encoding.Default.GetString(buffer.FirstSpan);
        }


        private string GenerateRandomGameCode()
        {
            var gameCode = string.Empty;
            var random = new Random();
            gameCode += (char) random.Next(65, 90); // A-Z
            //gameCode += (char) random.Next(65, 90);
            //gameCode += (char) random.Next(65, 90);
            //gameCode += (char) random.Next(65, 90);

            return gameCode;
        }
    }
}