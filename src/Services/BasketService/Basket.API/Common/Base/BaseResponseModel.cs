namespace Basket.API.Common.Base
{
    public class BaseResponseModel<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; }
    }
}
