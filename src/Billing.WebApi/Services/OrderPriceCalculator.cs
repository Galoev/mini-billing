using Billing.WebApi.Client.Models;
using Billing.WebApi.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Billing.WebApi.Services
{
    public class OrderPriceCalculator : IOrderPriceCalculator
    {
        private readonly IGoodsRepository goodsRepository;

        public OrderPriceCalculator(IGoodsRepository goodsRepository)
        {
            this.goodsRepository = goodsRepository;
        }

        public decimal CalculateOrderPrice(List<OrderGoodDto> orderGoods)
        {
            var listOfOrderGoodIds = orderGoods.Select(g => g.Id).ToList();
            var resultWithGoodsInfo = goodsRepository.Get(g => listOfOrderGoodIds.Contains(g.Id));

            if (resultWithGoodsInfo.IsSuccess)
            {
                return resultWithGoodsInfo.Value.Sum(g =>
                {
                    var orderGood = orderGoods.FirstOrDefault(og => og.Id == g.Id);
                    return g.UnitPrice * orderGood.Quantity;
                });
            }

            return 0.0M;
        }
    }
}
