using System.ComponentModel.DataAnnotations;

namespace Order.Domain.Entities
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public string GameID { get; set; }
        public string GameName { get; set; }
        public string GameAuthor { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string UserID { get; set; }
        public bool IsPaid { get; set; }
    }
}
