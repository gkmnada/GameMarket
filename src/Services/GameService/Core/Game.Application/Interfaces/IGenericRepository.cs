using Game.Application.Common.Base;

namespace Game.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task CreateAsync(T entity);
    }
}
