using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Order.API.Clients;
using Order.Application.Common.Base;
using Order.Application.Interfaces;
using Order.Application.Models;
using Order.Application.Settings;
using Order.Persistence.Context;
using System.Globalization;
using System.Security.Claims;

namespace Order.API.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly OrderContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public string UserID;
        private readonly IOrderRepository _orderRepository;
        private readonly GrpcMyGameClient _myGameClient;

        public PaymentService(OrderContext context, IHttpContextAccessor httpContextAccessor, IOrderRepository orderRepository, GrpcMyGameClient myGameClient)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            UserID = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            _orderRepository = orderRepository;
            _myGameClient = myGameClient;
        }

        public async Task<BaseResponseModel> PaymentAsync(PaymentForm paymentForm)
        {
            var order = await _orderRepository.GetOrderByUserAsync(UserID);

            decimal price = 0;

            foreach (var item in order)
            {
                price += item.Price;
            }

            string formattedPrice = price.ToString("F2", CultureInfo.InvariantCulture);

            Options options = new Options();
            options.ApiKey = MerchantSettings.ApiKey;
            options.SecretKey = MerchantSettings.SecretKey;
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "123456789";
            request.Price = formattedPrice;
            request.PaidPrice = formattedPrice;
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = paymentForm.CardHolderName;
            paymentCard.CardNumber = paymentForm.CardNumber;
            paymentCard.ExpireMonth = paymentForm.ExpireMonth;
            paymentCard.ExpireYear = paymentForm.ExpireYear;
            paymentCard.Cvc = paymentForm.CVV;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = "John";
            buyer.Surname = "Doe";
            buyer.GsmNumber = "+905350000000";
            buyer.Email = "email@email.com";
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();

            foreach (var item in order)
            {
                BasketItem firstBasketItem = new BasketItem();

                firstBasketItem.Id = item.GameID.ToString();
                firstBasketItem.Name = item.GameName;
                firstBasketItem.Category1 = "Game";
                firstBasketItem.Category2 = "Online Game";
                firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                firstBasketItem.Price = item.Price.ToString("F2", CultureInfo.InvariantCulture);
                basketItems.Add(firstBasketItem);
            }

            request.BasketItems = basketItems;

            Payment payment = Payment.Create(request, options);

            if (payment.Status == "success")
            {

                foreach (var item in order)
                {
                    var isPaid = await _orderRepository.UpdateIsPaidAsync(item);
                    var check = _myGameClient.SaveMyGame(UserID, item.GameID);

                    if (!isPaid || !check)
                    {
                        return new BaseResponseModel
                        {
                            IsSuccess = false,
                            Message = "Payment is not successful"
                        };
                    }
                }

                return new BaseResponseModel
                {
                    IsSuccess = true,
                    Message = "Payment is successful"
                };
            }

            return new BaseResponseModel
            {
                IsSuccess = false,
                Message = "Payment is not successful"
            };
        }
    }
}
