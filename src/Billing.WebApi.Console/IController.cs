using System;
using System.Collections.Generic;
using Billing.WebApi.Console.Models;

namespace Billing.WebApi.Console
{
    public interface IController
    {
        void creatCustomer(Customer customer);
        IEnumerable<Good> getGoods();
        void createOrder(Order order);
        IEnumerable<Order> getOrders();
    }
}
