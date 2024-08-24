using AutoMapper;
using MediatR;
using Order.Application.Features.Mediator.Queries.OrderQueries;
using Order.Application.Features.Mediator.Results.OrderResults;
using Order.Application.Interfaces;

namespace Order.Application.Features.Mediator.Handlers.OrderHandlers
{
    public class GetOrderByUserQueryHandler : IRequestHandler<GetOrderByUserQuery, List<GetOrderByUserQueryResult>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderByUserQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<GetOrderByUserQueryResult>> Handle(GetOrderByUserQuery request, CancellationToken cancellationToken)
        {
            var values = await _orderRepository.GetOrderByUserAsync(request.Id);
            return _mapper.Map<List<GetOrderByUserQueryResult>>(values);
        }
    }
}
