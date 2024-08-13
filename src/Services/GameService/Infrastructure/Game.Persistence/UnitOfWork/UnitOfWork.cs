using Game.Application.Interfaces;
using Game.Application.UnitOfWork;
using Game.Persistence.Context;
using Game.Persistence.Repositories;

namespace Game.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameContext _context;
        private IGameRepository _gameRepository;
        private ICategoryRepository _categoryRepository;

        public UnitOfWork(GameContext context)
        {
            _context = context;
        }

        public IGameRepository Games => _gameRepository ?? new GameRepository(_context);

        public ICategoryRepository Categories => _categoryRepository ?? new CategoryRepository(_context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
