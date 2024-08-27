using Order.Application.Common.Base;
using Order.Application.Models;

namespace Order.API.Services
{
    public interface IPaymentService
    {
        Task<BaseResponseModel> PaymentAsync(PaymentForm paymentForm);
    }
}
