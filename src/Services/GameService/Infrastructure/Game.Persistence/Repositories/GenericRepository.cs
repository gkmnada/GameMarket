using Game.Application.Interfaces;
using Game.Persistence.Context;

namespace Game.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly GameContext _context;

        public GenericRepository(GameContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
    }
}
