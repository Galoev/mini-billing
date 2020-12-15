using System;
using System.Collections.Generic;
using Billing.WebApi.Console.Models;

namespace Billing.WebApi.Console
{
    public class Controller: IController
    {
        private List<Customer> customers;
        private List<Order> orders;
        private List<Component> components;
        private List<Good> goods;

        public Controller()
        {
            customers = new List<Customer>();            
            orders = new List<Order>();
            components = new List<Component>();
            goods = new List<Good>();

            init();
        }

        public void creatCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void createOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Good> getGoods()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> getOrders()
        {
            return orders;
        }

        private void init()
        {
            for (int i = 0; i < 10; i++)
            {
                customers.Add(new Customer
                {
                    Name = $"Customer_{i}",
                    Phone = $"{i}{i}{i}{i}{i}{i}",
                    AdditionalInfo = "Some Additional Info"
                });
            }

            for (int i = 0; i < 10; i++)
            {

                components.Add(new Component
                {
                    UnitPrice = i,
                    Description = $"Component_{i}",
                    QuantityType = QuantityType.Kilogram
                });
            }

            for (int i = 0; i < 5; i++)
            {
                goods.Add(new Good
                {
                    QuantityType = QuantityType.Litre,
                    UnitPrice = i,
                    Description = $"Good_{i}",
                    Components = components
                });
            }

            for (int i = 0; i < 5; i++)
            {
                orders.Add(new Order
                {
                    Customer = customers[i],
                    OrderDate = DateTime.Now,
                    Price = i,
                    PaymentStatus = PaymentStatus.Paid,
                    DeliveryStatus = DeliveryStatus.Delivering
                });
            }
        }
    }
}
