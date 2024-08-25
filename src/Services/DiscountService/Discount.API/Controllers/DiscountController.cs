using Discount.API.Models;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _repository;

        public DiscountController(IDiscountRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCoupon([FromBody] DiscountModel discount)
        {
            var response = await _repository.CreateCouponAsync(discount);
            return Ok(response);
        }
    }
}
