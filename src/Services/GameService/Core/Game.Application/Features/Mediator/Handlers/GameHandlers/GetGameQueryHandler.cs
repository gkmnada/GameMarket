using AutoMapper;
using Game.Application.Features.Mediator.Queries.GameQueries;
using Game.Application.Features.Mediator.Results.GameResults;
using Game.Application.UnitOfWork;
using MediatR;

namespace Game.Application.Features.Mediator.Handlers.GameHandlers
{
    public class GetGameQueryHandler : IRequestHandler<GetGameQuery, List<GetGameQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetGameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetGameQueryResult>> Handle(GetGameQuery request, CancellationToken cancellationToken)
        {
            var values = await _unitOfWork.Games.GetAllAsync(cancellationToken);
            return _mapper.Map<List<GetGameQueryResult>>(values);
        }
    }
}
