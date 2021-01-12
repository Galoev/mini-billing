using Billing.WebApi.Repositories.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Billing.WebApi.Models;
using Billing.WebApi.Client.Utility;
using System.Collections.Generic;

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

            var orderDbo = new OrderDbo()
            {
                CustomerId = orderToCreate.Customer.Id,
                CreationDate = orderToCreate.CreationDate,
                Price = orderToCreate.Price,
                PaymentStatus = orderToCreate.PaymentStatus,
                DeliveryStatus = orderToCreate.DeliveryStatus
            };

            var orderGoodLinks = orderToCreate.Goods.Select(g => new OrderGoodLinkDbo
            {
                OrderId = orderDbo.Id,
                GoodId = g.Id,
                Quantity = g.Quantity
            }).ToList();

            orderDbo.OrderGoods = orderGoodLinks;
            billingContext.Orders.Add(orderDbo);
            var createdRows = billingContext.SaveChanges();

            if (createdRows > 0)
            {
                orderToCreate.Id = orderDbo.Id;
                orderToCreate.Customer.Name = customer.Name;
                orderToCreate.Customer.Phone = customer.Phone;
                orderToCreate.Customer.AdditionalInfo = customer.AdditionalInfo;

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
            var orderDbo = billingContext.Orders.Include(o => o.OrderGoods)
                .FirstOrDefault(o => o.Id == orderToDeleteId);
            if (orderDbo == null)
            {
                return new Result<Order> {
                    IsSuccess = false, 
                    Message = $"Order with id {orderToDeleteId} not found!",
                };
            }

            var customerOfDeletedOrder = billingContext.Customers
                .FirstOrDefault(c => c.Id == orderDbo.CustomerId);

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
                    Id = orderDbo.CustomerId,
                    Name = customerOfDeletedOrder.Name,
                    Phone = customerOfDeletedOrder.Phone,
                    AdditionalInfo = customerOfDeletedOrder.AdditionalInfo
                },
                CreationDate = orderDbo.CreationDate,
                Price = orderDbo.Price,
                PaymentStatus = orderDbo.PaymentStatus,
                DeliveryStatus = orderDbo.DeliveryStatus,
                Goods = orderDbo.OrderGoods.Select(item => new OrderGood 
                { 
                    Id = item.GoodId,
                    Quantity = item.Quantity
                }).ToList()
            };

            billingContext.Orders.Remove(orderDbo);
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
            var orderDbo = billingContext.Orders.Include(o => o.OrderGoods)
                .FirstOrDefault(o => o.Id == orderId);
            if (orderDbo == null)
            {
                return new Result<Order>
                {
                    IsSuccess = false,
                    Message = $"Could not find order with id {orderId}!"
                };
            }

            var customerOfFoundOrder = billingContext.Customers
                .FirstOrDefault(c => c.Id == orderDbo.CustomerId);

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
                    Goods = orderDbo.OrderGoods.Select(item => new OrderGood 
                    {
                        Id = item.GoodId,
                        Quantity = item.Quantity
                    }).ToList()
                }
            };
        }

        public Result<List<Order>> Get()
        {
            var listOfOrders = billingContext.Orders.Include(o => o.OrderGoods)
                .Select(o => new Order
                {
                    Id = o.Id,
                    CreationDate = o.CreationDate,
                    Price = o.Price,
                    PaymentStatus = o.PaymentStatus,
                    DeliveryStatus = o.DeliveryStatus,
                    Customer = new Customer
                    {
                        Id = o.CustomerId
                    },
                    Goods = o.OrderGoods.Select(g => new OrderGood
                    {
                        Id = g.GoodId,
                        Quantity = g.Quantity
                    }).ToList()
                }).ToList();

            return new Result<List<Order>>
            {
                IsSuccess = true,
                Message = $"List of orders successfully found!",
                Value = listOfOrders
            };
        }

        // Уточнить, что нужно обновлять
        public Result<Order> Update(Order orderToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
