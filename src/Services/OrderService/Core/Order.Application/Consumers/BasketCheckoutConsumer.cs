using AutoMapper;
using Game.Contracts.Events;
using MassTransit;
using MediatR;
using Order.Application.Interfaces;

namespace Order.Application.Consumers
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckout>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public BasketCheckoutConsumer(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task Consume(ConsumeContext<BasketCheckout> context)
        {
            try
            {
                var values = _mapper.Map<Domain.Entities.Order>(context.Message);

                await _orderRepository.CreateOrderAsync(values);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
