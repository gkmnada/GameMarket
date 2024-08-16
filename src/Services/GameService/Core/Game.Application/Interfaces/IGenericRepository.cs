using Game.Application.Common.Base;

namespace Game.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(string id, CancellationToken cancellationToken);
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
    }
}
