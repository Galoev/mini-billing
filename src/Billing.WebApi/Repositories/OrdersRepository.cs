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

            var order = new OrderDbo()
            {
                CustomerId = orderToCreate.Customer.Id,
                CreationDate = orderToCreate.CreationDate,
                PaymentStatus = orderToCreate.PaymentStatus,
                DeliveryStatus = orderToCreate.DeliveryStatus
            };

            var goods = billingContext.Goods.Where(item => orderToCreate.Goods.Any(g => g.Id == item.Id));
            var rangeToAdd = goods.Select(item => new OrderGoodLinkDbo
            {
                Quantity = orderToCreate.Goods.FirstOrDefault(g => g.Id == item.Id).Quantity
            });

            billingContext.OrderGoods.AddRange(rangeToAdd);
            billingContext.SaveChanges();

            return 0;
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
                var order = new Order
                {
                    Id = orderEntity.Id,
                    CreationDate = orderEntity.CreationDate,
                    Price = orderEntity.Price,
                    PaymentStatus = orderEntity.PaymentStatus,
                    DeliveryStatus = orderEntity.DeliveryStatus
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
