﻿using AutoMapper;
using Game.Application.Common.Base;
using Game.Contracts.Events;
using Game.Application.Features.Mediator.Commands.GameCommands;
using Game.Application.UnitOfWork;
using MassTransit;
using MediatR;

namespace Game.Application.Features.Mediator.Handlers.GameHandlers
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, BaseResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateGameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<BaseResponseModel> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Game>(request);

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
