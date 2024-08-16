using Game.Application.Interfaces;
using Game.Persistence.Context;
using Microsoft.EntityFrameworkCore;

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

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await Task.CompletedTask;
        }

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().FindAsync(id, cancellationToken);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await Task.CompletedTask;
        }
    }
}
