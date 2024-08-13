using Game.Application.Common.Base;
using MediatR;

namespace Game.Application.Features.Mediator.Commands.CategoryCommands
{
    public class CreateCategoryCommand : IRequest<BaseResponseModel>
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
