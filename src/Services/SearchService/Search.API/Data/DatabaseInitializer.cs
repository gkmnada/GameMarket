using MongoDB.Entities;
using MongoDB.Driver;
using Search.API.Models.Game;
using Microsoft.Extensions.Options;
using Search.API.Settings;

namespace Search.API.Data
{
    public class DatabaseInitializer
    {
        public static async Task InitializeAsync(WebApplication app)
        {
            try
            {
                var settings = app.Services.GetRequiredService<IOptions<DatabaseSettings>>().Value;
                await DB.InitAsync(settings.DatabaseName, MongoClientSettings.FromConnectionString(settings.ConnectionString));

                await DB.Index<GameItem>()
                    .Key(x => x.GameName, KeyType.Text)
                    .Key(x => x.Description, KeyType.Text)
                    .Key(x => x.GameAuthor, KeyType.Text).CreateAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
