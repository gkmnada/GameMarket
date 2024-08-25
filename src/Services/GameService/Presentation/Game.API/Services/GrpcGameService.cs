using Game.API.protos;
using Game.Persistence.Context;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Game.API.Services
{
    public class GrpcGameService : GrpcGame.GrpcGameBase
    {
        private readonly GameContext _context;

        public GrpcGameService(GameContext context)
        {
            _context = context;
        }

        public override async Task<GetGameResponse> GetGame(GetGameRequest request, ServerCallContext context)
        {
            Console.WriteLine("GetGame called");

            var values = await _context.Games.FirstOrDefaultAsync(x => x.Id == request.GameID && x.UserID == request.UserID);

            var response = new GetGameResponse
            {
                Game = new GetGameModel
                {
                    GameName = values.GameName,
                    Price = Convert.ToDouble(values.Price),
                    VideoPath = values.VideoPath,
                    Description = values.Description,
                    MinimumSystemRequirements = values.MinimumSystemRequirements,
                    RecommendedSystemRequirements = values.RecommendedSystemRequirements,
                    UserID = values.UserID,
                    CategoryID = values.CategoryID
                }
            };

            return response;
        }
    }
}
