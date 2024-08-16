using AutoMapper;
using Game.Application.Common.Base;
using Game.Application.Features.Mediator.Commands.GameCommands;
using Game.Application.UnitOfWork;
using Game.Contracts.Events;
using MassTransit;
using MediatR;

namespace Game.Application.Features.Mediator.Handlers.GameHandlers
{
    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, BaseResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public DeleteGameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<BaseResponseModel> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            var values = await _unitOfWork.Games.GetByIdAsync(request.Id, cancellationToken);

            await _unitOfWork.Games.DeleteAsync(values);
            await _publishEndpoint.Publish(_mapper.Map<GameDeleted>(values));
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new BaseResponseModel
            {
                Data = values,
                Message = "Deleted Successfully",
                IsSuccess = true
            };
        }
    }
}
