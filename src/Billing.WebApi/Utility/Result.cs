namespace Billing.WebApi.Utility
{
    public class Result<T>    
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Value { get; set; }
    }
}
