using AutoMapper;
using Game.Contracts.Events;
using Game.Application.Features.Mediator.Commands.CategoryCommands;
using Game.Application.Features.Mediator.Commands.GameCommands;
using Game.Domain.Entities;
using Game.Application.Features.Mediator.Results.GameResults;
using Game.Application.Models;

namespace Game.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Game Mapping
            CreateMap<Domain.Entities.Game, CreateGameCommand>().ReverseMap();
            CreateMap<Domain.Entities.Game, UpdateGameCommand>().ReverseMap();
            CreateMap<Domain.Entities.Game, GameCreated>().ReverseMap();
            CreateMap<Domain.Entities.Game, GameUpdated>().ReverseMap();
            CreateMap<Domain.Entities.Game, GameDeleted>().ReverseMap();
            CreateMap<Domain.Entities.Game, GetGameQueryResult>().ReverseMap();
            CreateMap<Domain.Entities.Game, GetGameByIdQueryResult>().ReverseMap();

            // MyGame Mapping
            CreateMap<MyGame, MyGameModel>().ReverseMap();

            // Category Mapping
            CreateMap<Category, CreateCategoryCommand>().ReverseMap();
        }
    }
}
