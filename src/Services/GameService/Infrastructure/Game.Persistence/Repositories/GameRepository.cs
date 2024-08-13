using Game.Application.Interfaces;
using Game.Persistence.Context;

namespace Game.Persistence.Repositories
{
    public class GameRepository : GenericRepository<Domain.Entities.Game>, IGameRepository
    {
        private readonly GameContext _context;

        public GameRepository(GameContext context) : base(context)
        {
            _context = context;
        }
    }
}
