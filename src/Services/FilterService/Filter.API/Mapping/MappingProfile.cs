using AutoMapper;
using Filter.API.Models.Game;
using Game.Contracts.Events;

namespace Filter.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GameCreated, GameFilterItem>().ReverseMap();
        }
    }
}
