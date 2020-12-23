using System;
using Billing.WebApi.Console.Models;

namespace Billing.WebApi.Console
{
    public interface IEditBilling
    {
        void CreatCustomer(Customer customer);
        void CreateOrder(CreateOrder order);
    }
}
