using Game.Domain.Base;

namespace Game.Domain.Entities
{
    public class Game : BaseEntity
    {
        public string GameName { get; set; }
        public string GameAuthor { get; set; }
        public decimal Price { get; set; }
        public string VideoPath { get; set; }
        public string GameInfo { get; set; }
        public string MinimumSystemRequirements { get; set; }
        public string RecommendedSystemRequirements { get; set; }
        public string Description { get; set; }
        public string UserID { get; set; }
        public string CategoryID { get; set; }
        public Category Category { get; set; }
        public ICollection<GameImage> GameImages { get; set; }
        public ICollection<MyGame> MyGames { get; set; }
    }
}
