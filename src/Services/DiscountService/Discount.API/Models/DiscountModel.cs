namespace Discount.API.Models
{
    public class DiscountModel
    {
        public string CouponCode { get; set; }
        public int Amount { get; set; }
        public string GameID { get; set; }
        public string? UserID { get; set; }
    }
}
