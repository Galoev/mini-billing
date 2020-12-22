using Billing.WebApi.Client.Models;
using System.Linq;

namespace Billing.WebApi.Models.Converter
{
    public class OrderConverter : IOrderConverter
    {
        public Order FromCreateDto(CreateOrderDto orderDto)
        {
            return new Order
            {
                CreationDate = orderDto.CreationDate,
                PaymentStatus = orderDto.PaymentStatus,
                DeliveryStatus = orderDto.DeliveryStatus,
                Customer = new Customer
                {
                    Id = orderDto.CustomerId
                },
                Goods = orderDto.Goods.Select(item => FromOrderGoodDto(item)).ToList()
            };   
        }

        private OrderGood FromOrderGoodDto(OrderGoodDto orderGoodDto)
        {
            return new OrderGood
            {
                Id = orderGoodDto.Id,
                Quantity = orderGoodDto.Quantity
            };
        }

        private OrderGoodDto ToOrderGoodDto(OrderGood orderGood)
        {
            return new OrderGoodDto
            {
                Id = orderGood.Id,
                Quantity = orderGood.Quantity
            };
        }

        public GetOrderDto ToGetDto(Order order)
        {
            return new GetOrderDto
            {
                Id = order.Id,
                CreationDate = order.CreationDate,
                Price = order.Price,
                PaymentStatus = order.PaymentStatus,
                DeliveryStatus = order.DeliveryStatus,
                Customer = new CustomerDto
                {
                    Id = order.Customer.Id,
                    Name = order.Customer.Name,
                    Phone = order.Customer.Phone,
                    AdditionalInfo = order.Customer.AdditionalInfo
                },
                Goods = order.Goods.Select(item => ToOrderGoodDto(item)).ToList()
            };
        }
    }
}
