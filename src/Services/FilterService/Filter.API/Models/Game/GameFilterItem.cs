using System.Text.Json.Serialization;

namespace Filter.API.Models.Game
{
    public class GameFilterItem
    {
        [JsonPropertyName("gameID")]
        public string GameID { get; set; }

        [JsonPropertyName("gameName")]
        public string GameName { get; set; }

        [JsonPropertyName("gameAuthor")]
        public string GameAuthor { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("gameInfo")]
        public string GameInfo { get; set; }

        [JsonPropertyName("minimumSystemRequirements")]
        public string MinimumSystemRequirements { get; set; }

        [JsonPropertyName("recommendedSystemRequirements")]
        public string RecommendedSystemRequirements { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("categoryID")]
        public string CategoryID { get; set; }
    }
}
