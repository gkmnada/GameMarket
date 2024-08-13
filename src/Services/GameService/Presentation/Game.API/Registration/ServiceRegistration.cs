using Game.Application.Interfaces;
using Game.Application.UnitOfWork;
using Game.Persistence.Repositories;
using Game.Persistence.UnitOfWork;

namespace Game.API.Registration
{
    public static class ServiceRegistration
    {
        public static void ApiService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
        }
    }
}
