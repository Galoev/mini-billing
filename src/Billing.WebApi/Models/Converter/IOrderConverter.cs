using Billing.WebApi.Client.Models;

namespace Billing.WebApi.Models.Converter
{
    public interface IOrderConverter
    {
        public GetOrderDto ToGetDto(Order order);
        public Order FromCreateDto(CreateOrderDto orderDto);
        public Order FromUpdateDto(UpdateOrderDto updateOrderDto);
    }
}
