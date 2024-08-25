using Discount.API.Models;
using Discount.API.protos;
using Grpc.Net.Client;

namespace Discount.API.Services
{
    public class GrpcGameClient
    {
        private readonly ILogger<GrpcGameClient> _logger;
        private readonly IConfiguration _configuration;

        public GrpcGameClient(ILogger<GrpcGameClient> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public GameModel GetGame(string gameID, string userID)
        {
            _logger.LogWarning("Calling GRPC Protobuf Service");

            var channel = GrpcChannel.ForAddress(_configuration["GameGPRC"]);
            var client = new GrpcGame.GrpcGameClient(channel);
            var request = new GetGameRequest { GameID = gameID, UserID = userID };

            try
            {
                var response = client.GetGame(request);

                GameModel gameModel = new GameModel
                {
                    GameName = response.Game.GameName,
                    Price = Convert.ToDecimal(response.Game.Price),
                    VideoPath = response.Game.VideoPath,
                    MinimumSystemRequirements = response.Game.MinimumSystemRequirements,
                    RecommendedSystemRequirements = response.Game.RecommendedSystemRequirements,
                    Description = response.Game.Description,
                    UserID = response.Game.UserID,
                    CategoryID = response.Game.CategoryID
                };

                return gameModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
