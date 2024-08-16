using AutoMapper;
using Game.Contracts.Events;
using MassTransit;
using MongoDB.Entities;
using Search.API.Models.Game;

namespace Search.API.Consumers.Game
{
    public class GameUpdatedConsumer : IConsumer<GameUpdated>
    {
        private readonly IMapper _mapper;

        public GameUpdatedConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<GameUpdated> context)
        {
            try
            {
                var gameItem = _mapper.Map<GameItem>(context.Message);

                var result = await DB.Update<GameItem>()
                    .Match(x => x.ID == context.Message.Id)
                    .Modify(x => x.GameName, gameItem.GameName)
                    .Modify(x => x.GameAuthor, gameItem.GameAuthor)
                    .Modify(x => x.Price, gameItem.Price)
                    .Modify(x => x.GameInfo, gameItem.GameInfo)
                    .Modify(x => x.MinimumSystemRequirements, gameItem.MinimumSystemRequirements)
                    .Modify(x => x.RecommendedSystemRequirements, gameItem.RecommendedSystemRequirements)
                    .Modify(x => x.Description, gameItem.Description)
                    .Modify(x => x.CategoryID, gameItem.CategoryID)
                    .ExecuteAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
