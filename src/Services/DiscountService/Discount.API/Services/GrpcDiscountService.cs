using Discount.API.Context;
using Discount.API.protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Discount.API.Services
{
    public class GrpcDiscountService : GrpcDiscount.GrpcDiscountBase
    {
        private readonly DiscountContext _context;

        public GrpcDiscountService(DiscountContext context)
        {
            _context = context;
        }

        public override async Task<GetDiscountResponse> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var discount = await _context.Coupons.FirstOrDefaultAsync(x => x.CouponCode == request.CouponCode);

            var response = new GetDiscountResponse
            {
                Discount = new GetDiscountModel
                {
                    CouponCode = discount.CouponCode,
                    Amount = discount.Amount,
                    GameID = discount.GameID,
                    UserID = discount.UserID
                }
            };

            return response;
        }
    }
}
