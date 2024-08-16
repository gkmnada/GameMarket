using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;

namespace Search.API.Models.Game
{
    public class GameItem : Entity
    {
        public string GameName { get; set; }
        public string GameAuthor { get; set; }
        public decimal Price { get; set; }
        public string GameInfo { get; set; }
        public string MinimumSystemRequirements { get; set; }
        public string RecommendedSystemRequirements { get; set; }
        public string Description { get; set; }
        public string CategoryID { get; set; }
    }
}
