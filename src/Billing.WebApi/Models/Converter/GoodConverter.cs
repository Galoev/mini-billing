using Billing.WebApi.Client.Models;
using System.Linq;

namespace Billing.WebApi.Models.Converter
{
    public class GoodConverter : IGoodConverter
    {
        public Good FromCreateDto(CreateGoodDto createGoodDto)
        {
            return new Good
            {
                QuantityType = createGoodDto.QuantityType,
                UnitPrice = createGoodDto.UnitPrice,
                Description = createGoodDto.Description,
                Components = createGoodDto.Components.Select(c => new GoodComponent 
                { 
                    Id = c.Id,
                    Quantity = c.Quantity
                }).ToList()
            };
        }

        public Good FromUpdateDto(UpdateGoodDto updateGoodDto)
        {
            return new Good
            {
                Id = updateGoodDto.Id,
                QuantityType = updateGoodDto.QuantityType,
                UnitPrice = updateGoodDto.UnitPrice,
                Description = updateGoodDto.Description,
                Components = updateGoodDto.Components.Select(c => new GoodComponent
                {
                    Id = c.Id,
                    Quantity = c.Quantity
                }).ToList()
            };
        }

        public GetGoodDto ToGetDto(Good good)
        {
            return new GetGoodDto
            {
                Id = good.Id,
                QuantityType = good.QuantityType,
                Description = good.Description,
                UnitPrice = good.UnitPrice,
                Components = good.Components.Select(c => new GoodComponentDto 
                { 
                    Id = c.Id,
                    Quantity = c.Quantity
                }).ToList()
            };
        }
    }
}
