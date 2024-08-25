using AutoMapper;
using Game.Application.Common.Base;
using Game.Contracts.Events;
using Game.Application.Features.Mediator.Commands.GameCommands;
using Game.Application.UnitOfWork;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Game.Application.Features.Mediator.Handlers.GameHandlers
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, BaseResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string UserID;

        public CreateGameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IPublishEndpoint publishEndpoint, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _httpContextAccessor = httpContextAccessor;
            UserID = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }

        public async Task<BaseResponseModel> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Game>(request);

            entity.UserID = UserID;

            await _unitOfWork.Games.CreateAsync(entity);
            await _publishEndpoint.Publish(_mapper.Map<GameCreated>(entity));
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
