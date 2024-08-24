using MediatR;
using Order.Application.Features.Mediator.Results.OrderResults;

namespace Order.Application.Features.Mediator.Queries.OrderQueries
{
    public class GetOrderByUserQuery : IRequest<List<GetOrderByUserQueryResult>>
    {
        public string Id { get; set; }

        public GetOrderByUserQuery(string id)
        {
            Id = id;
        }
    }
}
