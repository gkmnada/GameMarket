using AutoMapper;
using Game.Application.Features.Mediator.Commands.CategoryCommands;
using Game.Application.Features.Mediator.Commands.GameCommands;
using Game.Domain.Entities;

namespace Game.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Game Mapping
            CreateMap<Domain.Entities.Game, CreateGameCommand>().ReverseMap();

            // Category Mapping
            CreateMap<Category, CreateCategoryCommand>().ReverseMap();
        }
    }
}
