using Billing.WebApi.Models;

namespace Billing.WebApi.Repositories
{
    public class OrderRepositoryResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public Order Value { get; set; }
    }
}
