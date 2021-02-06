using Billing.WebApi.Client.Models;

namespace Billing.WebApi.Models.Converter
{
    public interface IGoodConverter
    {
        public Good FromCreateDto(CreateGoodDto createGoodDto);
        public Good FromUpdateDto(UpdateGoodDto updateGoodDto);
        public GetGoodDto ToGetDto(Good good);
    }
}
