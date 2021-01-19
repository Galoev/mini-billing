using Billing.WebApi.Client.Models;
using Billing.WebApi.Console.Models;
using System.Linq;

namespace Billing.WebApi.Console.Converters
{
    public class OrderConverter
    {
        public static Order FromDto(GetOrderDto orderDto)
        {
            return new Order
            {
                Id = orderDto.Id,
                Customer = CustomerConverter.FromDto(orderDto.Customer),
                CreationDate = orderDto.CreationDate,
                PaymentStatus = orderDto.PaymentStatus,
                DeliveryStatus = orderDto.DeliveryStatus,
                Price = orderDto.Price,
                Goods = orderDto.Goods.Select(g => GoodConverter.FromDto(g)).ToList()
            };
        }

        public static CreateOrderDto ToDto(CreateOrder order)
        {
            return new CreateOrderDto
            {
                CustomerId = order.CustomerId,
                CreationDate = order.CreationDate,
                DeliveryStatus = order.DeliveryStatus,
                PaymentStatus = order.PaymentStatus,
                Goods = order.Goods.Select(g => GoodConverter.ToDto(g)).ToList()
            };
        }

        public static InfoOrder InfoOrderFromDto(GetOrderDto orderDto) 
        {
            return new InfoOrder
            {
                Id = orderDto.Id,
                CustomerId = orderDto.Customer.Id,
                CreationDate = orderDto.CreationDate,
                DeliveryStatus = orderDto.DeliveryStatus,
                PaymentStatus = orderDto.PaymentStatus,
                Price = orderDto.Price
            };
        }
    }
}
