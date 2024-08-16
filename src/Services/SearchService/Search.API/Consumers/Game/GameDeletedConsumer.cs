using AutoMapper;
using Game.Contracts.Events;
using MassTransit;
using MongoDB.Entities;
using Search.API.Models.Game;

namespace Search.API.Consumers.Game
{
    public class GameDeletedConsumer : IConsumer<GameDeleted>
    {
        private readonly IMapper _mapper;

        public GameDeletedConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<GameDeleted> context)
        {
            try
            {
                var result = await DB.DeleteAsync<GameItem>(context.Message.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
