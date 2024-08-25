using AutoMapper;
using Game.Application.Features.Mediator.Queries.GameQueries;
using Game.Application.Features.Mediator.Results.GameResults;
using Game.Application.UnitOfWork;
using MediatR;

namespace Game.Application.Features.Mediator.Handlers.GameHandlers
{
    public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, GetGameByIdQueryResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetGameByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetGameByIdQueryResult> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _unitOfWork.Games.GetByIdAsync(request.Id, cancellationToken);
            return _mapper.Map<GetGameByIdQueryResult>(values);
        }
    }
}
