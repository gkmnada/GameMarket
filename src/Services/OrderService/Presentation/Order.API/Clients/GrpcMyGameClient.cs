using Grpc.Net.Client;
using Order.API.protos;

namespace Order.API.Clients
{
    public class GrpcMyGameClient
    {
        private readonly IConfiguration _configuration;

        public GrpcMyGameClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SaveMyGame(string userID, string gameID)
        {
            var channel = GrpcChannel.ForAddress(_configuration["MyGameGPRC"]);
            var client = new GrpcMyGame.GrpcMyGameClient(channel);

            var request = new GetMyGameRequest
            {
                UserID = userID,
                GameID = gameID
            };

            var response = client.GetMyGame(request);

            if (!string.IsNullOrEmpty(response.MyGames.GameID))
            {
                return true;
            }

            return false;
        }
    }
}
