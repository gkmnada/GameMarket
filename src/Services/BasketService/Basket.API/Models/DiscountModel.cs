namespace Basket.API.Models
{
    public class DiscountModel
    {
        public int CouponID { get; set; }
        public string CouponCode { get; set; }
        public int Amount { get; set; }
        public string GameID { get; set; }
        public string UserID { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
