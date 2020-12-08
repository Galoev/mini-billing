using Billing.WebApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Billing.WebApi.Models;

namespace Billing.WebApi.DAL
{
    public class EFCoreOrdersRepository : IOrdersRepository
    {
        private readonly BillingContext _billingContext;
        public EFCoreOrdersRepository(BillingContext billingContext)
        {
            _billingContext = billingContext;
        }
        public decimal Create(Order orderToCreate)
        {
            var customer = _billingContext.Customers.First(c => c.Id == orderToCreate.Customer.Id);

            var price = orderToCreate.Goods.Aggregate(0.0M, 
                (sumPrice, nextGoods) => sumPrice + nextGoods.QuantityInOrder * nextGoods.Price); ;

            var order = new OrderEntity()
            {
                CustomerId = orderToCreate.Customer.Id,
                OrderDate = orderToCreate.OrderDate,
                Price = price,
                PaymentStatus = Convert.ToInt32(orderToCreate.PaymentStatus),
                DeliverStatus = Convert.ToInt32(orderToCreate.DeliverStatus),
                Customer = customer
            };

            var goods = _billingContext.Goods.Where(item => orderToCreate.Goods.Any(g => g.Id == item.Id));
            var rangeToAdd = goods.Select(item => new OrderGoods
            {
                Order = order,
                Goods = item,
                Quantity = orderToCreate.Goods.First(g => g.Id == item.Id).QuantityInOrder
            });

            _billingContext.OrderGoods.AddRange(rangeToAdd);
            _billingContext.SaveChanges();

            return price;
        }

        public void Delete(Order orderToDelete)
        {
            var order = _billingContext.Orders.First(item => item.Id == orderToDelete.Id);
            _billingContext.Orders.Remove(order);
            _billingContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var order = _billingContext.Orders.Find(id);
            _billingContext.Orders.Remove(order);
            _billingContext.SaveChanges();
        }

        public List<Order> Get(Expression<Func<Order, bool>> filter = null, Func<IQueryable<Order>, IOrderedQueryable<Order>> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public Order Get(int orderId)
        {
            var orderEntity = _billingContext.Orders.First(item => item.Id == orderId);

            var customer = new Customer
            {
                Id = orderEntity.Customer.Id,
                Name = orderEntity.Customer.Name,
                Phone = orderEntity.Customer.Phone,
                AdditionalInfo = orderEntity.Customer.AdditionalInfo
            };

            var goods = orderEntity.OrderGoods.Select(item => new Goods {
                Id = item.Goods.Id,
                Price = item.Goods.Price,
                QuantityInOrder = item.Quantity,
                Description = item.Goods.Description
            }).ToList(); 

            return new Order
            {
                Id = orderEntity.Id,
                OrderDate = orderEntity.OrderDate,
                Price = orderEntity.Price,
                PaymentStatus = (PaymentStatus)orderEntity.PaymentStatus,
                DeliverStatus = (DeliverStatus)orderEntity.DeliverStatus,
                Customer = customer,
                Goods = goods
            };
        }

        // проговорить про компоненты
        public List<Component> GetComponents()
        {
            throw new NotImplementedException();
        }

        // Уточнить, что нужно обновлять
        public void Update(Order orderToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
