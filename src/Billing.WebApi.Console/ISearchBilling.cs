using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Billing.WebApi.Console.Models;

namespace Billing.WebApi.Console
{
    public interface ISearchBilling
    {
        List<Good> GetGoods();
        List<OrderGood> GetOrderGoods();
        List<InfoGood> GetInfoGoods();
        List<InfoOrder> GetInfoOrders();
        List<Order> GetOrders();
        Task<List<Customer>> GetCustomers();
    }
}
