using AutoMapper;
using Game.Contracts.Events;
using Order.Application.Features.Mediator.Results.OrderResults;

namespace Order.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.Order, GetOrderByUserQueryResult>().ReverseMap();
            CreateMap<Domain.Entities.Order, BasketCheckout>().ReverseMap();
        }
    }
}
