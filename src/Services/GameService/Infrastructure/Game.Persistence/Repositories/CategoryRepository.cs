using Game.Application.Interfaces;
using Game.Domain.Entities;
using Game.Persistence.Context;

namespace Game.Persistence.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly GameContext _context;

        public CategoryRepository(GameContext context) : base(context)
        {
            _context = context;
        }
    }
}
