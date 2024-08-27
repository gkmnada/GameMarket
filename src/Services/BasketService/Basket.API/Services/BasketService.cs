using AutoMapper;
using Basket.API.Clients;
using Basket.API.Common.Base;
using Basket.API.Models;
using Game.Contracts.Events;
using MassTransit;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Security.Claims;

namespace Basket.API.Services
{
    public class BasketService : IBasketService
    {
        private readonly IDatabase _database;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;
        public string UserID;
        public string connectionString;
        private readonly GrpcDiscountClient _grpcDiscountClient;

        public BasketService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IPublishEndpoint publishEndpoint, IMapper mapper, GrpcDiscountClient grpcDiscountClient)
        {
            connectionString = configuration.GetValue<string>("RedisDatabase");
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(connectionString);
            _database = connection.GetDatabase();
            _httpContextAccessor = httpContextAccessor;
            UserID = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
            _grpcDiscountClient = grpcDiscountClient;
        }

        public async Task<BaseResponseModel<BasketModel>> AddBasket(BasketModel basketModel)
        {
            if (basketModel is not null)
            {
                var data = JsonConvert.SerializeObject(basketModel);
                var response = await _database.ListRightPushAsync(UserID, data);

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
            var data = await _database.ListGetByIndexAsync(UserID, index);
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
            var data = await _database.ListRangeAsync(UserID);
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
            var willDelete = await _database.ListGetByIndexAsync(UserID, index);
            var response = await _database.ListRemoveAsync(UserID, willDelete);
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
            await _database.ListSetByIndexAsync(UserID, index, data);

            return new BaseResponseModel<BasketModel>
            {
                Data = basketModel,
                IsSuccess = true,
                Message = "Basket item updated successfully"
            };
        }

        public async Task<BaseResponseModel<bool>> BasketCheckout()
        {
            List<Checkout> checkouts = new List<Checkout>();

            var data = await _database.ListRangeAsync(UserID);
            foreach (var item in data)
            {
                Checkout checkout = new Checkout();
                var basketModel = JsonConvert.DeserializeObject<BasketModel>(item);
                checkout.GameID = basketModel.GameID;
                checkout.GameName = basketModel.GameName;
                checkout.GameAuthor = basketModel.GameAuthor;
                checkout.Price = basketModel.Price;
                checkout.Description = basketModel.Description;
                checkout.UserID = UserID;
                checkouts.Add(checkout);
            }

            if (checkouts.Count > 0)
            {
                foreach (var item in checkouts)
                {
                    await _publishEndpoint.Publish(_mapper.Map<BasketCheckout>(item));
                }

                await _database.KeyDeleteAsync(UserID);

                return new BaseResponseModel<bool>
                {
                    Data = true,
                    IsSuccess = true,
                    Message = "Basket checked out successfully"
                };
            }

            return new BaseResponseModel<bool>
            {
                Data = false,
                IsSuccess = false,
                Message = "Basket could not be checked out"
            };
        }

        public async Task<BaseResponseModel<bool>> ImplementCoupon(string couponCode, long index)
        {
            var discount = _grpcDiscountClient.GetDiscount(couponCode);

            if (discount != null)
            {
                var response = await _database.ListGetByIndexAsync(UserID, index);
                var basketModel = JsonConvert.DeserializeObject<BasketModel>(response);
                basketModel.Price = basketModel.Price - (basketModel.Price * discount.Amount / 100);
                await _database.ListSetByIndexAsync(UserID, index, JsonConvert.SerializeObject(basketModel));

                return new BaseResponseModel<bool>
                {
                    Data = true,
                    IsSuccess = true,
                    Message = "Coupon implemented successfully"
                };
            }
            else
            {
                return new BaseResponseModel<bool>
                {
                    Data = false,
                    IsSuccess = false,
                    Message = "Coupon could not be implemented"
                };
            }
        }
    }
}
