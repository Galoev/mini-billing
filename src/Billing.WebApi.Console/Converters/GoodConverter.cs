using Billing.WebApi.Client.Models;
using Billing.WebApi.Console.Models;
using System.Linq;

namespace Billing.WebApi.Console.Converters
{
    public class GoodConverter
    {
        public static Good FromDto(GetGoodDto goodDto) => new Good
        {
            Id = goodDto.Id,
            UnitPrice = goodDto.UnitPrice,
            QuantityType = goodDto.QuantityType,
            Description = goodDto.Description ?? string.Empty,
            Components = goodDto.Components
                    .Select(c => ComponentConverter.FromDto(c))
                    .ToList()
        };

        public static CreateGoodDto ToDto(Good good) => new CreateGoodDto
        {
            UnitPrice = good.UnitPrice,
            QuantityType = good.QuantityType,
            Description = string.IsNullOrEmpty(good.Description)
                    ? null
                    : good.Description,
            Components = good.Components.Select(c => ComponentConverter.ToDto(c)).ToList()
        };

        public static OrderGood FromDto(OrderGoodDto orderGoodDto) => new OrderGood
        {
            Id = orderGoodDto.Id,
            Quantity = orderGoodDto.Quantity
        };

        public static OrderGoodDto ToDto(OrderGood orderGood) => new OrderGoodDto
        {
            Id = orderGood.Id,
            Quantity = orderGood.Quantity
        };

        public static GoodInfo InfoGoodFromDto(GetGoodDto goodDto) => new GoodInfo
        {
            Id = goodDto.Id,
            UnitPrice = goodDto.UnitPrice,
            QuantityType = goodDto.QuantityType,
            Description = goodDto.Description ?? string.Empty
        };
    }
}
