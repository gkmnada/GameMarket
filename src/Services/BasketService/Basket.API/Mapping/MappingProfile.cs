using AutoMapper;
using Basket.API.Models;
using Game.Contracts.Events;

namespace Basket.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BasketCheckout, Checkout>().ReverseMap();
        }
    }
}
