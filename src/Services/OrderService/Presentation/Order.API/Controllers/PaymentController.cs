using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.API.Services;
using Order.Application.Models;

namespace Order.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(PaymentForm paymentForm)
        {
            var response = await _paymentService.PaymentAsync(paymentForm);
            return Ok(response);
        }
    }
}
