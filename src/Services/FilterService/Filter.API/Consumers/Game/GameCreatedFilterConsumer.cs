using AutoMapper;
using Elastic.Clients.Elasticsearch;
using Filter.API.Models.Game;
using Game.Contracts.Events;
using MassTransit;

namespace Filter.API.Consumers.Game
{
    public class GameCreatedFilterConsumer : IConsumer<GameCreated>
    {
        private readonly IMapper _mapper;
        private readonly ElasticsearchClient _elasticsearchClient;

        public GameCreatedFilterConsumer(IMapper mapper, ElasticsearchClient elasticsearchClient, IConfiguration configuration)
        {
            _mapper = mapper;
            _elasticsearchClient = elasticsearchClient;
            indexName = configuration["Elasticsearch:IndexName"] ?? "filter-game";
        }

        public string indexName;

        public async Task Consume(ConsumeContext<GameCreated> context)
        {
            try
            {
                var values = _mapper.Map<GameFilterItem>(context.Message);
                values.GameID = context.Message.Id;

                var elasticSearch = await _elasticsearchClient.IndexAsync(values, x => x.Index(indexName));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
