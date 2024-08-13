using Game.Domain.Base;

namespace Game.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
