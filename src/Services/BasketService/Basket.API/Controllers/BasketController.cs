using Basket.API.Models;
using Basket.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBasket(BasketModel basketModel)
        {
            var response = await _basketService.AddBasket(basketModel);
            return Ok(response);
        }

        [HttpGet("GetBasketItem")]
        public async Task<IActionResult> GetBasketItem(long index)
        {
            var response = await _basketService.GetBasketItem(index);
            return Ok(response);
        }

        [HttpGet("GetBasketItems")]
        public async Task<IActionResult> GetBasketItems()
        {
            var response = await _basketService.GetBasketItems();
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveBasketItem(long index)
        {
            var response = await _basketService.RemoveBasketItem(index);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBasketItem(BasketModel basketModel, long index)
        {
            var response = await _basketService.UpdateBasketItem(basketModel, index);
            return Ok(response);
        }

        [HttpPost("BasketCheckout")]
        public async Task<IActionResult> BasketCheckout()
        {
            var response = await _basketService.BasketCheckout();
            return Ok(response);
        }

        [HttpPut("ImplementCoupon")]
        public async Task<IActionResult> ImplementCoupon(string couponCode, long index)
        {
            var response = await _basketService.ImplementCoupon(couponCode, index);
            return Ok(response);
        }
    }
}
