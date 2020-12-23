using System;
using System.Collections.Generic;
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
        List<Customer> GetCustomers();
    }
}
