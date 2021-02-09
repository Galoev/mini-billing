using Billing.WebApi.Client.Models;
using System.Collections.Generic;

namespace Billing.WebApi.Services
{
    public interface IOrderPriceCalculator
    {
        public decimal CalculateOrderPrice(List<OrderGoodDto> orderGoods);
    }
}
