using AutoMapper;
using Game.Application.Common.Base;
using Game.Application.Features.Mediator.Commands.GameCommands;
using Game.Application.UnitOfWork;
using MediatR;

namespace Game.Application.Features.Mediator.Handlers.GameHandlers
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, BaseResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateGameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponseModel> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Game>(request);

            await _unitOfWork.Games.CreateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new BaseResponseModel
            {
                Data = entity,
                Message = "Created Successfully",
                IsSuccess = true
            };
        }
    }
}
