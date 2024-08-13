namespace Game.Domain.Base
{
    public class BaseEntity
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString("D");
            CreatedAt = DateTime.Now;
        }
    }
}
