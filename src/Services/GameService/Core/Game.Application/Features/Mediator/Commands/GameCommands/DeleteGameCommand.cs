using Game.Application.Common.Base;
using MediatR;

namespace Game.Application.Features.Mediator.Commands.GameCommands
{
    public class DeleteGameCommand : IRequest<BaseResponseModel>
    {
        public string Id { get; set; }

        public DeleteGameCommand(string id)
        {
            Id = id;
        }
    }
}
