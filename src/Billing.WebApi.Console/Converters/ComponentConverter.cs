using Billing.WebApi.Client.Models;
using Billing.WebApi.Console.Models;

namespace Billing.WebApi.Console.Converters
{
    public class ComponentConverter
    {
        public static Component FromDto(GetComponentDto componentDto) => new Component
        {
            Id = componentDto.Id,
            UnitPrice = componentDto.UnitPrice,
            QuantityType = componentDto.QuantityType,
            Description = componentDto.Description ?? string.Empty
        };

        public static CreateComponentDto ToDto(Component component) => new CreateComponentDto
        {
            UnitPrice = component.UnitPrice,
            QuantityType = component.QuantityType,
            Description = string.IsNullOrEmpty(component.Description)
                    ? null
                    : component.Description
        };

        public static GoodComponent FromDto(GoodComponentDto goodComponentDto) => new GoodComponent
        {
            Id = goodComponentDto.Id,
            Quantity = goodComponentDto.Quantity
        };

        public static GoodComponentDto ToDto(GoodComponent goodComponent) => new GoodComponentDto
        {
            Id = goodComponent.Id,
            Quantity = goodComponent.Quantity
        };
    }
}
