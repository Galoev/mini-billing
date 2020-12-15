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
            var customer = billingContext.Customers.FirstOrDefault(c => c.Id == orderToCreate.Customer.Id);
            customer = customer ??= new CustomerDbo
            {
                Name = orderToCreate.Customer.Name,
                Phone = orderToCreate.Customer.Phone,
                AdditionalInfo = orderToCreate.Customer.AdditionalInfo
            };

            orderToCreate.Goods.Select(item =>
                item.UnitPrice = GetPriceForGood(item.Id, item.QuantityUnit));

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
                Quantity = orderToCreate.Goods.FirstOrDefault(g => g.Id == item.Id).Quantity,
                QuantityUnit = orderToCreate.Goods.First(g => g.Id == item.Id).QuantityUnit
            });

            billingContext.OrderGoods.AddRange(rangeToAdd);
            billingContext.SaveChanges();

            return price;
        }

        private decimal GetPriceForGood(Guid id, QuantityType qtype)
        {
            var priceRecord = billingContext.UnitGoodPrices.FirstOrDefault(p =>
                    p.GoodId == id && p.QuantityUnit == qtype);
            return priceRecord == null ? 0.0M : priceRecord.UnitPrice;
        }

        public void Delete(Order orderToDelete)
        {
            var order = billingContext.Orders.First(item => item.Id == orderToDelete.Id);
            billingContext.Orders.Remove(order);
            billingContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var order = billingContext.Orders.Find(id);
            billingContext.Orders.Remove(order);
            billingContext.SaveChanges();
        }

        public OrderRepositoryResult Get(Guid orderId)
        {
            var orderEntity = billingContext.Orders.FirstOrDefault(item => item.Id == orderId);
            OrderRepositoryResult result;
            if (orderEntity == null)
            {
                result = new OrderRepositoryResult
                {
                    IsSuccess = false,
                    Message = $"Could not find order with id : {orderId}"
                };
            }
            else
            {
                var customer = new Customer
                {
                    Id = orderEntity.Customer.Id,
                    Name = orderEntity.Customer.Name,
                    Phone = orderEntity.Customer.Phone,
                    AdditionalInfo = orderEntity.Customer.AdditionalInfo
                };

                var goods = orderEntity.OrderGoods.Select(item => new OrderGood
                {
                    Id = item.GoodId,
                    UnitPrice = GetPriceForGood(item.GoodId, item.QuantityUnit),
                    Quantity = item.Quantity,
                    QuantityUnit = item.QuantityUnit
                }).ToList();

                var order = new Order
                {
                    Id = orderEntity.Id,
                    OrderDate = orderEntity.OrderDate,
                    Price = orderEntity.Price,
                    PaymentStatus = orderEntity.PaymentStatus,
                    DeliveryStatus = orderEntity.DeliverStatus,
                    Customer = customer,
                    Goods = goods
                };

                result = new OrderRepositoryResult
                {
                    IsSuccess = true,
                    Message = "Success",
                    Value = order
                };
            }
            return result;
        }

        // Уточнить, что нужно обновлять
        public void Update(Order orderToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
