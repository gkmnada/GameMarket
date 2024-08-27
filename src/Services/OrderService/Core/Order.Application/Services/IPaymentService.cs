using Order.Application.Common.Base;
using Order.Application.Models;

namespace Order.Application.Services
{
    public interface IPaymentService
    {
        Task<BaseResponseModel> PaymentAsync(PaymentForm paymentForm);
    }
}
