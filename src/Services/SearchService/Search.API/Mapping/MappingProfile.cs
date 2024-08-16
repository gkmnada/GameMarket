using AutoMapper;
using Game.Contracts.Events;
using Search.API.Models.Game;

namespace Search.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GameItem, GameCreated>().ReverseMap();
            CreateMap<GameItem, GameDeleted>().ReverseMap();
            CreateMap<GameItem, GameUpdated>().ReverseMap();
        }
    }
}
