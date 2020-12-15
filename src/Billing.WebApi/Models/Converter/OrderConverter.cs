using Billing.WebApi.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Billing.WebApi.Models.Converter
{
    public class OrderConverter : IOrderConverter
    {
        public Order FromCreateDto(CreateOrderDto orderDto)
        {
            return new Order
            {
                OrderDate = orderDto.OrderDate,
                PaymentStatus = (PaymentStatus)orderDto.PaymentStatus,
                DeliveryStatus = (DeliveryStatus)orderDto.DeliveryStatus,
                Customer = new Customer
                {
                    Name = orderDto.Customer.Name,
                    Phone = orderDto.Customer.Phone,
                    AdditionalInfo = orderDto.Customer.AdditionalInfo
                },
                Goods = (ICollection<OrderGood>)orderDto.Goods.Select(item => new OrderGood
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    QuantityUnit = (QuantityType)item.QuantityUnit
                })
            };   
        }

        public GetOrderDto ToGetDto(Order order)
        {
            return new GetOrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Price = order.Price,
                PaymentStatus = Convert.ToInt32(order.PaymentStatus),
                DeliveryStatus = Convert.ToInt32(order.DeliveryStatus),
                Customer = new CustomerDto
                {
                    Name = order.Customer.Name,
                    Phone = order.Customer.Phone,
                    AdditionalInfo = order.Customer.AdditionalInfo
                },
                Goods = (ICollection<OrderGoodDto>)order.Goods.Select(item => new OrderGoodDto
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    QuantityUnit = Convert.ToInt32(item.QuantityUnit)
                })
            };
        }
    }
}
