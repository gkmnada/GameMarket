using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using Search.API.Helpers;
using Search.API.Models.Game;

namespace Search.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<GameItem>>> SearchItems([FromQuery] SearchParams searchParams)
        {
            var query = DB.PagedSearch<GameItem, GameItem>();

            if (!string.IsNullOrEmpty(searchParams.search))
            {
                query.Match(MongoDB.Entities.Search.Full, searchParams.search).SortByTextScore();
            }

            var result = await query.ExecuteAsync();

            return Ok(new
            {
                result = result.Results
            });
        }
    }
}
