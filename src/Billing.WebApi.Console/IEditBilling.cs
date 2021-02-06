using System.Threading.Tasks;
using Billing.WebApi.Client.Utility;
using Billing.WebApi.Console.Models;

namespace Billing.WebApi.Console
{
    public interface IEditBilling
    {
        Task<Result<Customer>> CreateCustomer(Customer customer);
        Task<Result<Order>> CreateOrder(CreateOrder order);
        Task<Result<Good>> CreateGood(CreateGood good);
        Task<Result<Component>> CreateComponent(CreateComponent component);
    }
}
