using Elastic.Clients.Elasticsearch;

namespace Filter.API.Extensions
{
    public static class ElasticExtension
    {
        public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var elasticSearchUrl = configuration["Elasticsearch:Url"];

            var settings = new ElasticsearchClientSettings(new Uri(elasticSearchUrl));
            var client = new ElasticsearchClient(settings);

            services.AddSingleton(client);
        }
    }
}
