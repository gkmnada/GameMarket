using AutoMapper;
using Game.Application.Common.Base;
using Game.Application.Features.Mediator.Commands.GameCommands;
using Game.Application.UnitOfWork;
using Game.Contracts.Events;
using MassTransit;
using MediatR;

namespace Game.Application.Features.Mediator.Handlers.GameHandlers
{
    public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, BaseResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public UpdateGameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<BaseResponseModel> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
        {
            var values = await _unitOfWork.Games.GetByIdAsync(request.Id, cancellationToken);

            var entity = _mapper.Map(request, values);
            await _unitOfWork.Games.UpdateAsync(entity);
            await _publishEndpoint.Publish(_mapper.Map<GameUpdated>(entity));
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new BaseResponseModel
            {
                Data = entity,
                Message = "Updated Successfully",
                IsSuccess = true
            };
        }
    }
}
