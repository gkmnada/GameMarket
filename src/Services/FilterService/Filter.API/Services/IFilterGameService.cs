using Filter.API.Models.Game;

namespace Filter.API.Services
{
    public interface IFilterGameService
    {
        Task<List<GameFilterItem>> SearchAsync(GameFilterItem gameFilterItem);
    }
}
