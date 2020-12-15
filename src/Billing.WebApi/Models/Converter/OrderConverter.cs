using Billing.WebApi.Client.Models;
using System.Collections.Generic;
using System.Linq;

namespace Billing.WebApi.Models.Converter
{
    public class OrderConverter : IOrderConverter
    {
        public Order FromDto(OrderDto orderDto)
        {
            return new Order
            {
                OrderDate = orderDto.OrderDate,
                Price = orderDto.Price,
                PaymentStatus = orderDto.PaymentStatus,
                DeliveryStatus = orderDto.DeliveryStatus,
                Customer = new Customer
                {
                    Name = orderDto.Customer.Name,
                    Phone = orderDto.Customer.Phone,
                    AdditionalInfo = orderDto.Customer.AdditionalInfo
                },
                Goods = (ICollection<OrderGood>)orderDto.Goods.Select(item => new OrderGood
                {
                    Id = item.Id,
                    Quantity = item.Quantity
                })
            };   
        }

        public OrderDto ToDto(Order order)
        {
            return new OrderDto
            {
                OrderDate = order.OrderDate,
                Price = order.Price,
                PaymentStatus = order.PaymentStatus,
                DeliveryStatus = order.DeliveryStatus,
                Customer = new CustomerDto
                {
                    Name = order.Customer.Name,
                    Phone = order.Customer.Phone,
                    AdditionalInfo = order.Customer.AdditionalInfo
                },
                Goods = (ICollection<OrderGoodDto>)order.Goods.Select(item => new OrderGoodDto
                {
                    Id = item.Id,
                    Quantity = item.Quantity
                })
            };
        }
    }
}
