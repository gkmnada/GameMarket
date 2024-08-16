using Filter.API.Models.Game;
using Filter.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Filter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly IFilterGameService _filterGameService;

        public FilterController(IFilterGameService filterGameService)
        {
            _filterGameService = filterGameService;
        }

        [HttpPost]
        public async Task<IActionResult> FilterAsync(GameFilterItem gameFilterItem)
        {
            var result = await _filterGameService.SearchAsync(gameFilterItem);
            return Ok(result);
        }
    }
}
