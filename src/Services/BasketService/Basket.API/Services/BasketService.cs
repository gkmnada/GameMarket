﻿using Basket.API.Common.Base;
using Basket.API.Models;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Security.Claims;

namespace Basket.API.Services
{
    public class BasketService : IBasketService
    {
        private readonly IDatabase _database;
        public string connectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public string user;

        public BasketService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            connectionString = configuration.GetValue<string>("RedisDatabase");
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(connectionString);
            _database = connection.GetDatabase();
            _httpContextAccessor = httpContextAccessor;
            user = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }

        public async Task<BaseResponseModel<BasketModel>> AddBasket(BasketModel basketModel)
        {
            if (basketModel is not null)
            {
                var data = JsonConvert.SerializeObject(basketModel);
                var response = await _database.ListRightPushAsync(user, data);

                return new BaseResponseModel<BasketModel>
                {
                    Data = basketModel,
                    IsSuccess = true,
                    Message = "Basket item added successfully"
                };
            }
            else
            {
                return new BaseResponseModel<BasketModel>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "Basket item could not be added"
                };
            }
        }

        public async Task<BaseResponseModel<BasketModel>> GetBasketItem(long index)
        {
            var data = await _database.ListGetByIndexAsync(user, index);
            if (data.HasValue)
            {
                var basketModel = JsonConvert.DeserializeObject<BasketModel>(data);
                return new BaseResponseModel<BasketModel>
                {
                    Data = basketModel,
                    IsSuccess = true,
                    Message = "Basket item retrieved successfully"
                };
            }
            else
            {
                return new BaseResponseModel<BasketModel>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "Basket item could not be retrieved"
                };
            }
        }

        public async Task<BaseResponseModel<List<BasketModel>>> GetBasketItems()
        {
            var data = await _database.ListRangeAsync(user);
            if (data.Any())
            {
                var basketModels = new List<BasketModel>();
                foreach (var item in data)
                {
                    var basketModel = JsonConvert.DeserializeObject<BasketModel>(item);
                    basketModels.Add(basketModel);
                }

                return new BaseResponseModel<List<BasketModel>>
                {
                    Data = basketModels,
                    IsSuccess = true,
                    Message = "Basket items retrieved successfully"
                };
            }
            else
            {
                return new BaseResponseModel<List<BasketModel>>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "Basket items could not be retrieved"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> RemoveBasketItem(long index)
        {
            var willDelete = await _database.ListGetByIndexAsync(user, index);
            var response = await _database.ListRemoveAsync(user, willDelete);
            if (response > 0)
            {
                return new BaseResponseModel<bool>
                {
                    Data = true,
                    IsSuccess = true,
                    Message = "Basket item removed successfully"
                };
            }
            else
            {
                return new BaseResponseModel<bool>
                {
                    Data = false,
                    IsSuccess = false,
                    Message = "Basket item could not be removed"
                };
            }
        }

        public async Task<BaseResponseModel<BasketModel>> UpdateBasketItem(BasketModel basketModel, long index)
        {
            var data = JsonConvert.SerializeObject(basketModel);
            await _database.ListSetByIndexAsync(user, index, data);

            return new BaseResponseModel<BasketModel>
            {
                Data = basketModel,
                IsSuccess = true,
                Message = "Basket item updated successfully"
            };
        }
    }
}
