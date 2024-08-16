using Game.Application.Features.Mediator.Results.GameResults;
using MediatR;

namespace Game.Application.Features.Mediator.Queries.GameQueries
{
    public class GetGameQuery : IRequest<List<GetGameQueryResult>>
    {
    }
}
