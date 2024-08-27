using AutoMapper;
using Game.API.protos;
using Game.Application.Models;
using Game.Domain.Entities;
using Game.Persistence.Context;
using Grpc.Core;

namespace Game.API.Services
{
    public class GrpcMyGameService : GrpcMyGame.GrpcMyGameBase
    {
        private readonly GameContext _context;
        private readonly IMapper _mapper;

        public GrpcMyGameService(GameContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public override async Task<GetMyGameResponse> GetMyGame(GetMyGameRequest request, ServerCallContext context)
        {
            var myGame = new MyGameModel { GameID = request.GameID, UserID = request.UserID };

            var entity = _mapper.Map<MyGame>(myGame);

            await _context.MyGames.AddAsync(entity);
            await _context.SaveChangesAsync();

            var response = new GetMyGameResponse
            {
                MyGames = new GetMyGameModel { GameID = entity.GameID, UserID = entity.UserID }
            };

            return response;
        }
    }
}
