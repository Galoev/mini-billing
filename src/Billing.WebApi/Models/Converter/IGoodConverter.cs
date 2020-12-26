using Billing.WebApi.Client.Models;

namespace Billing.WebApi.Models.Converter
{
    public interface IGoodConverter
    {
        public Good FromCreateGoodDto(CreateGoodDto createGoodDto);
        public GetGoodDto ToGetGoodDto(Good good);
    }
}
