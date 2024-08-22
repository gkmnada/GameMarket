namespace Game.Contracts.Events
{
    public class BasketCheckout
    {
        public string GameID { get; set; }
        public string GameName { get; set; }
        public string GameAuthor { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string UserID { get; set; }
    }
}
