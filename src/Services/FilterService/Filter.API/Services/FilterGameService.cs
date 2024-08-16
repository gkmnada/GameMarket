using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Filter.API.Models.Game;

namespace Filter.API.Services
{
    public class FilterGameService : IFilterGameService
    {
        private readonly ElasticsearchClient _elasticsearchClient;

        public FilterGameService(ElasticsearchClient elasticsearchClient, IConfiguration configuration)
        {
            _elasticsearchClient = elasticsearchClient;
            indexName = configuration["Elasticsearch:IndexName"] ?? "filter-game";
        }

        public string indexName;

        public async Task<List<GameFilterItem>> SearchAsync(GameFilterItem gameFilterItem)
        {
            List<Action<QueryDescriptor<GameFilterItem>>> listQuery = new();

            if (gameFilterItem is null)
            {
                listQuery.Add(q => q.MatchAll());
                return await CalculateResultSet(listQuery);
            }

            if (!string.IsNullOrEmpty(gameFilterItem.Price.ToString()) && gameFilterItem.Price != 0)
            {
                listQuery.Add((q) => q.Range(m => m.NumberRange(f => f.Field(a => a.Price).Gte(Convert.ToDouble(gameFilterItem.Price)))));
            }

            if (!string.IsNullOrEmpty(gameFilterItem.Price.ToString()) && gameFilterItem.Price != 0)
            {
                listQuery.Add((q) => q.Range(m => m.NumberRange(f => f.Field(a => a.Price).Lte(Convert.ToDouble(gameFilterItem.Price)))));
            }

            if (!string.IsNullOrEmpty(gameFilterItem.MinimumSystemRequirements))
            {
                string searchValue = "*" + gameFilterItem.MinimumSystemRequirements + "*";
                listQuery.Add((q) => q.Wildcard(m => m.Field(f => f.MinimumSystemRequirements).Value(searchValue)));
            }

            if (!string.IsNullOrEmpty(gameFilterItem.RecommendedSystemRequirements))
            {
                string searchValue = "*" + gameFilterItem.RecommendedSystemRequirements + "*";
                listQuery.Add((q) => q.Wildcard(m => m.Field(f => f.RecommendedSystemRequirements).Value(searchValue)));
            }

            if (!listQuery.Any())
            {
                listQuery.Add(q => q.MatchAll());
            }

            return await CalculateResultSet(listQuery);
        }

        private async Task<List<GameFilterItem>> CalculateResultSet(List<Action<QueryDescriptor<GameFilterItem>>> listQuery)
        {
            var result = await _elasticsearchClient.SearchAsync<GameFilterItem>(x => x.Index(indexName).Query(a => a.Bool(b => b.Must(listQuery.ToArray()))));
            return result.Documents.ToList();
        }
    }
}
