using Game.Domain.Base;

namespace Game.Domain.Entities
{
    public class MyGame : BaseEntity
    {
        public string GameID { get; set; }
        public Game Game { get; set; }
        public string UserID { get; set; }
    }
}
