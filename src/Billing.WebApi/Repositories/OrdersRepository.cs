using Billing.WebApi.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Billing.WebApi.Models;

namespace Billing.WebApi.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly BillingContext billingContext;
        public OrdersRepository(BillingContext billingContext)
        {
            this.billingContext = billingContext;
        }
        public decimal Create(Order orderToCreate)
        {
            var customer = billingContext.Customers.First(c => c.Id == orderToCreate.Customer.Id);

            var price = orderToCreate.Goods.Aggregate(0.0M, 
                (sumPrice, nextGoods) => sumPrice + nextGoods.Quantity * nextGoods.UnitPrice); ;

            var order = new OrderDbo()
            {
                CustomerId = orderToCreate.Customer.Id,
                OrderDate = orderToCreate.OrderDate,
                Price = price,
                PaymentStatus = orderToCreate.PaymentStatus,
                DeliverStatus = orderToCreate.DeliveryStatus,
                Customer = customer
            };

            var goods = billingContext.Goods.Where(item => orderToCreate.Goods.Any(g => g.Id == item.Id));
            var rangeToAdd = goods.Select(item => new OrderGoodLinkDbo
            {
                Order = order,
                Good = item,
                Quantity = orderToCreate.Goods.First(g => g.Id == item.Id).Quantity
            });

            billingContext.OrderGoods.AddRange(rangeToAdd);
            billingContext.SaveChanges();

            return price;
        }

        public void Delete(Order orderToDelete)
        {
            var order = billingContext.Orders.First(item => item.Id == orderToDelete.Id);
            billingContext.Orders.Remove(order);
            billingContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var order = billingContext.Orders.Find(id);
            billingContext.Orders.Remove(order);
            billingContext.SaveChanges();
        }

        public Order Get(int orderId)
        {
            var orderEntity = billingContext.Orders.First(item => item.Id == orderId);

            var customer = new Customer
            {
                Id = orderEntity.Customer.Id,
                Name = orderEntity.Customer.Name,
                Phone = orderEntity.Customer.Phone,
                AdditionalInfo = orderEntity.Customer.AdditionalInfo
            };

            var goods = orderEntity.OrderGoods.Select(item => new Good {
                Id = item.Goods.Id,
                Price = item.Goods.Price,
                Quantity = item.Quantity,
                Description = item.Goods.Description
            }).ToList(); 

            return new Order
            {
                Id = orderEntity.Id,
                OrderDate = orderEntity.OrderDate,
                Price = orderEntity.Price,
                PaymentStatus = orderEntity.PaymentStatus,
                DeliveryStatus = orderEntity.DeliverStatus,
                Customer = customer,
                Goods = goods
            };
        }

        // Уточнить, что нужно обновлять
        public void Update(Order orderToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
