using Basket.API.Models;
using Basket.API.protos;
using Grpc.Net.Client;

namespace Basket.API.Clients
{
    public class GrpcDiscountClient
    {
        private readonly ILogger<GrpcDiscountClient> _logger;
        private readonly IConfiguration _configuration;

        public GrpcDiscountClient(ILogger<GrpcDiscountClient> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public DiscountModel GetDiscount(string couponCode)
        {
            _logger.LogWarning("Calling GRPC Protobuf Service");

            var channel = GrpcChannel.ForAddress(_configuration["DiscountGRPC"]);
            var client = new GrpcDiscount.GrpcDiscountClient(channel);
            var request = new GetDiscountRequest { CouponCode = couponCode };

            var response = client.GetDiscount(request);

            DiscountModel discountModel = new DiscountModel
            {
                CouponCode = response.Discount.CouponCode,
                Amount = response.Discount.Amount,
                GameID = response.Discount.GameID,
                UserID = response.Discount.UserID
            };

            return discountModel;
        }
    }
}
