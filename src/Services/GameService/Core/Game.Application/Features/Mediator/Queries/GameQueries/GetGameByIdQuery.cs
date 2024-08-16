using Game.Application.Features.Mediator.Results.GameResults;
using MediatR;

namespace Game.Application.Features.Mediator.Queries.GameQueries
{
    public class GetGameByIdQuery : IRequest<GetGameByIdQueryResult>
    {
        public string Id { get; set; }

        public GetGameByIdQuery(string id)
        {
            Id = id;
        }
    }
}
