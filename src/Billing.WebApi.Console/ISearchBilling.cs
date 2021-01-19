using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.WebApi.Console.Models;

namespace Billing.WebApi.Console
{
    public interface ISearchBilling
    {
        Task<List<Good>> GetGoods();
        Task<List<Order>> GetOrders();
        Task<List<Customer>> GetCustomers();

        Task<List<InfoOrder>> GetInfoOrders();
        Task<List<InfoGood>> GetInfoGoods();
    }
}
