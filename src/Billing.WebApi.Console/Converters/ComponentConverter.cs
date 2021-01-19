using Billing.WebApi.Client.Models;
using Billing.WebApi.Console.Models;

namespace Billing.WebApi.Console.Converters
{
    public class ComponentConverter
    {
        public static Component FromDto(GetComponentDto componentDto) 
        {
            return new Component
            {
                Id = componentDto.Id,
                UnitPrice = componentDto.UnitPrice,
                QuantityType = componentDto.QuantityType,
                Description = componentDto.Description ?? ""
            };
        }

        public static CreateComponentDto ToDto(Component component)
        {
            return new CreateComponentDto
            {
                UnitPrice = component.UnitPrice,
                QuantityType = component.QuantityType,
                Description = string.IsNullOrEmpty(component.Description)
                    ? null
                    : component.Description
            };
        }

        public static GoodComponent FromDto(GoodComponentDto goodComponentDto) 
        {
            return new GoodComponent
            {
                Id = goodComponentDto.Id,
                Quantity = goodComponentDto.Quantity
            };
        }

        public static GoodComponentDto ToDto(GoodComponent goodComponent)
        {
            return new GoodComponentDto
            {
                Id = goodComponent.Id,
                Quantity = goodComponent.Quantity
            };
        }
    }
}
