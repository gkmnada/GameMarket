using Discount.API.Entities;
using Discount.API.Models;

namespace Discount.API.Repositories
{
    public interface IDiscountRepository
    {
        Task<bool> CreateCouponAsync(DiscountModel discount);
    }
}
