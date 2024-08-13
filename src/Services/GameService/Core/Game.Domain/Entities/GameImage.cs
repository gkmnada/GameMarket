using Game.Domain.Base;

namespace Game.Domain.Entities
{
    public class GameImage : BaseEntity
    {
        public string ImagePath { get; set; }
        public string GameID { get; set; }
        public Game Game { get; set; }
    }
}
