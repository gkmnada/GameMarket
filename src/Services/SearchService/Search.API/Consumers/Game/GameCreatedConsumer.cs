using AutoMapper;
using Game.Contracts.Events;
using MassTransit;
using Search.API.Models.Game;
using MongoDB.Entities;

namespace Search.API.Consumers.Game
{
    public class GameCreatedConsumer : IConsumer<GameCreated>
    {
        private readonly IMapper _mapper;

        public GameCreatedConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<GameCreated> context)
        {
            try
            {
                var gameItem = _mapper.Map<GameItem>(context.Message);
                await gameItem.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
