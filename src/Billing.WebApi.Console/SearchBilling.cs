using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billing.WebApi.Client.Clients;
using Billing.WebApi.Console.Models;

namespace Billing.WebApi.Console
{
    public class SearchBilling: ISearchBilling
    {
        private List<Customer> customers;
        private List<Order> orders;
        private List<InfoOrder> infoOrders;
        private List<Component> components;
        private List<Good> goods;
        private List<OrderGood> orderGoods;
        private List<InfoGood> infoGoods;

        private readonly CustomersClient customersClient;

        public SearchBilling()
        {
            customers = new List<Customer>();
            orders = new List<Order>();
            infoOrders = new List<InfoOrder>();
            components = new List<Component>();
            goods = new List<Good>();
            orderGoods = new List<OrderGood>();
            infoGoods = new List<InfoGood>();

            customersClient = new CustomersClient("https://localhost:44311");

            Init();
        }

        public async Task<List<Customer>> GetCustomers()
        {
            var resultFromClient = await customersClient.GetCustomersAsync();
            if (resultFromClient.IsSuccess)
            {
                return resultFromClient.Value.Select(c => new Customer
                {
                    Name = c.Name,
                    Phone = c.Phone,
                    AdditionalInfo = c.AdditionalInfo
                }).ToList();
            }
            return Enumerable.Empty<Customer>().ToList();
        }

        public List<Good> GetGoods()
        {
            return goods;
        }

        public List<InfoGood> GetInfoGoods()
        {
            return infoGoods;
        }

        public List<InfoOrder> GetInfoOrders()
        {
            return infoOrders;
        }

        public List<OrderGood> GetOrderGoods()
        {
            return orderGoods;
        }

        public List<Order> GetOrders()
        {
            return orders;
        }

        private void Init()
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
                infoGoods.Add(new InfoGood
                {
                    QuantityType = QuantityType.Litre,
                    UnitPrice = i,
                    Description = $"Good_{i}"
                });
            }

            for (int i = 0; i < 5; i++)
            {
                infoOrders.Add(new InfoOrder
                {
                    Customer = customers[i],
                    OrderDate = DateTime.Now,
                    Price = i,
                    PaymentStatus = PaymentStatus.Paid,
                    DeliveryStatus = DeliveryStatus.Delivering
                });
            }

            for (int i = 0; i < 5; i++)
            {
                orderGoods.Add(new OrderGood
                {
                    QuantityUnit = i,
                    UnitPrice = i,
                    Quantity = i
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
                    DeliveryStatus = DeliveryStatus.Delivering,
                    Goods = goods
                });
            }
        }
    }
}
