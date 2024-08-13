using Game.Application.Interfaces;

namespace Game.Application.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        IGameRepository Games { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
