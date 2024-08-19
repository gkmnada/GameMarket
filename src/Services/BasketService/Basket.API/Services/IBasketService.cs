using Basket.API.Common.Base;
using Basket.API.Models;

namespace Basket.API.Services
{
    public interface IBasketService
    {
        Task<BaseResponseModel<BasketModel>> AddBasket(BasketModel basketModel);
        Task<BaseResponseModel<BasketModel>> GetBasketItem(long index);
        Task<BaseResponseModel<List<BasketModel>>> GetBasketItems();
        Task<BaseResponseModel<bool>> RemoveBasketItem(long index);
        Task<BaseResponseModel<BasketModel>> UpdateBasketItem(BasketModel basketModel, long index);
    }
}
