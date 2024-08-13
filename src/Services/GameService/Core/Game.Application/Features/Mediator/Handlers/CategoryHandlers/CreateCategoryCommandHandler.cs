using AutoMapper;
using Game.Application.Common.Base;
using Game.Application.Features.Mediator.Commands.CategoryCommands;
using Game.Application.UnitOfWork;
using Game.Domain.Entities;
using MediatR;

namespace Game.Application.Features.Mediator.Handlers.CategoryHandlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, BaseResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponseModel> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Category>(request);

            await _unitOfWork.Categories.CreateAsync(entity);
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
