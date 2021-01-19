using System;
using System.Threading.Tasks;
using Billing.WebApi.Console.Models;

namespace Billing.WebApi.Console
{
    public interface IEditBilling
    {
        Task<Customer> CreateCustomer(Customer customer);
        Task<Order> CreateOrder(CreateOrder order);
    }
}
