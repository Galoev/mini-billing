using System;
using System.Collections.Generic;
using Billing.WebApi.Console.Models;

namespace Billing.WebApi.Console
{
    public interface ISearchBilling
    {
        IEnumerable<Good> GetGoods();
        IEnumerable<OrderGood> GetOrderGoods();
        IEnumerable<InfoOrder> GetInfoOrders();
        IEnumerable<Order> GetOrders();
        IEnumerable<Customer> GetCustomers();
    }
}
