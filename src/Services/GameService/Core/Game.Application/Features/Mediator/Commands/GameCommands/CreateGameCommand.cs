using Game.Application.Common.Base;
using MediatR;

namespace Game.Application.Features.Mediator.Commands.GameCommands
{
    public class CreateGameCommand : IRequest<BaseResponseModel>
    {
        public string GameName { get; set; }
        public string GameAuthor { get; set; }
        public decimal Price { get; set; }
        public string VideoPath { get; set; }
        public string GameInfo { get; set; }
        public string MinimumSystemRequirements { get; set; }
        public string RecommendedSystemRequirements { get; set; }
        public string Description { get; set; }
        public Guid CategoryID { get; set; }
    }
}
