using Discount.API.Context;
using Discount.API.Entities;
using Discount.API.Models;
using Discount.API.Services;
using System.Security.Claims;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly GrpcGameClient _grpcGameClient;
        private string UserID;

        public DiscountRepository(DiscountContext context, IHttpContextAccessor httpContextAccessor, GrpcGameClient grpcGameClient)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _grpcGameClient = grpcGameClient;
            UserID = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }

        public async Task<bool> CreateCouponAsync(DiscountModel discount)
        {
            var values = _grpcGameClient.GetGame(discount.GameID, UserID);

            Coupon coupon = new Coupon
            {
                CouponCode = discount.CouponCode,
                Amount = discount.Amount,
                GameID = discount.GameID,
                UserID = values.UserID,
            };

            await _context.Coupons.AddAsync(coupon);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
