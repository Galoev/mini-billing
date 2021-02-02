using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.WebApi.Client.Utility;
using Billing.WebApi.Console.Models;

namespace Billing.WebApi.Console
{
    public interface ISearchBilling
    {
        Task<Result<List<Good>>> GetGoods();
        Task<Result<List<Order>>> GetOrders();
        Task<Result<List<Customer>>> GetCustomers();

        Task<Result<List<OrderInfo>>> GetOrdersInfo();
        Task<Result<List<GoodInfo>>> GetGoodsInfo();
    }
}
