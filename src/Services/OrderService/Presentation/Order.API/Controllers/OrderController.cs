using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Order.Application.Features.Mediator.Queries.OrderQueries;
using System.Security.Claims;

namespace Order.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string UserID;

        public OrderController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            UserID = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderByUser()
        {
            var response = await _mediator.Send(new GetOrderByUserQuery(UserID));
            return Ok(response);
        }
    }
}
