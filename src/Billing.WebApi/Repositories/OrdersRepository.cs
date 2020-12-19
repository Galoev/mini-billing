using Billing.WebApi.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using Billing.WebApi.Models;
using Billing.WebApi.Utility;

namespace Billing.WebApi.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly BillingContext billingContext;
        public OrdersRepository(BillingContext billingContext)
        {
            this.billingContext = billingContext;
        }
        public Result<Order> Create(Order orderToCreate)
        {
            var customerId = orderToCreate.Customer.Id;
            var customer = billingContext.Customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                return new Result<Order>
                {
                    IsSuccess = false,
                    Message = $"Customer with id {customerId} not found!"
                };
            }

            var order = new OrderDbo()
            {
                CustomerId = orderToCreate.Customer.Id,
                CreationDate = orderToCreate.CreationDate,
                Price = orderToCreate.Price,
                PaymentStatus = orderToCreate.PaymentStatus,
                DeliveryStatus = orderToCreate.DeliveryStatus
            };

            billingContext.Orders.Add(order);
            var createdRows = billingContext.SaveChanges();

            if (createdRows > 0)
            {
                return new Result<Order>
                {
                    IsSuccess = true,
                    Message = "Order successfully created!",
                    Value = orderToCreate
                };
            }

            return new Result<Order>
            {
                IsSuccess = false,
                Message = "Order not created!"
            };
        }

        public Result<Order> Delete(Guid orderToDeleteId)
        {
            var order = billingContext.Orders.FirstOrDefault(item => item.Id == orderToDeleteId);
            if (order == null)
            {
                return new Result<Order> {
                    IsSuccess = false, 
                    Message = $"Order with id {orderToDeleteId} not found!",
                };
            }

            var customerOfDeletedOrder = billingContext.Customers.FirstOrDefault(item => item.Id == order.CustomerId);

            if (customerOfDeletedOrder == null)
            {
                return new Result<Order>
                {
                    IsSuccess = false,
                    Message = "Cannot find customer of deleted order!"
                };
            }

            var deletedOrder = new Order()
            {
                Customer = new Customer
                {
                    Id = order.CustomerId,
                    Name = customerOfDeletedOrder.Name,
                    Phone = customerOfDeletedOrder.Phone,
                    AdditionalInfo = customerOfDeletedOrder.AdditionalInfo
                },
                CreationDate = order.CreationDate,
                Price = order.Price,
                PaymentStatus = order.PaymentStatus,
                DeliveryStatus = order.DeliveryStatus,
                Goods = GetOrderGoods(order.Id)
            };

            billingContext.Orders.Remove(order);
            var deletedRows = billingContext.SaveChanges();

            if (deletedRows > 0)
            {
                return new Result<Order>
                {
                    IsSuccess = true,
                    Message = "Order successfully deleted!",
                    Value = deletedOrder
                };
            }
            return new Result<Order>
            {
                IsSuccess = false,
                Message = "Order not deleted!"
            };
        }

        public Result<Order> Get(Guid orderId)
        {
            var orderDbo = billingContext.Orders.FirstOrDefault(item => item.Id == orderId);
            if (orderDbo == null)
            {
                return new Result<Order>
                {
                    IsSuccess = false,
                    Message = $"Could not find order with id {orderId}!"
                };
            }

            var customerOfFoundOrder = billingContext.Customers.FirstOrDefault(c => c.Id == orderDbo.CustomerId);

            if (customerOfFoundOrder == null)
            {
                return new Result<Order>
                {
                    IsSuccess = false,
                    Message = "Cannot find customer of found order!"
                };
            }

            return new Result<Order> {
                IsSuccess = true,
                Message = $"Order with id {orderId} successfully found!",
                Value = new Order
                {
                    Id = orderDbo.Id,
                    CreationDate = orderDbo.CreationDate,
                    Price = orderDbo.Price,
                    PaymentStatus = orderDbo.PaymentStatus,
                    DeliveryStatus = orderDbo.DeliveryStatus,
                    Customer = new Customer
                    {
                        Id = orderDbo.CustomerId,
                        Name = customerOfFoundOrder.Name,
                        Phone = customerOfFoundOrder.Phone,
                        AdditionalInfo = customerOfFoundOrder.AdditionalInfo
                    },
                    Goods = GetOrderGoods(orderId)
                }
            };
        }

        // Уточнить, что нужно обновлять
        public Result<Order> Update(Order orderToUpdate)
        {
            throw new NotImplementedException();
        }

        private List<OrderGood> GetOrderGoods(Guid orderId)
        {
            var goodsIdsAndQuantitiesOfDeletedOrder = billingContext.OrderGoods
                .Where(item => item.OrderId == orderId)
                .ToDictionary(item => item.GoodId);
            return billingContext.Goods
                .Where(good => goodsIdsAndQuantitiesOfDeletedOrder.ContainsKey(good.Id))
                .Select(item => new OrderGood
                {
                    Id = item.Id,
                    QuantityType = item.QuantityType,
                    Quantity = goodsIdsAndQuantitiesOfDeletedOrder
                    .GetValueOrDefault(item.Id).Quantity
                }).ToList();
        }
    }
}
