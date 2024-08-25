using Game.Application.Features.Mediator.Commands.GameCommands;
using Game.Application.Features.Mediator.Queries.GameQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Game.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GameController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
            var response = await _mediator.Send(new GetGameQuery());
            return Ok(response);
        }

        [HttpGet("GetGameById")]
        public async Task<IActionResult> GetGameById(string id)
        {
            var response = await _mediator.Send(new GetGameByIdQuery(id));
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame(CreateGameCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGame(UpdateGameCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGame(DeleteGameCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
