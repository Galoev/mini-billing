using Billing.WebApi.Models;
using Billing.WebApi.Repositories.Models;
using Billing.WebApi.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Billing.WebApi.Repositories
{
    public class GoodRepository : IGoodRepository
    {
        private readonly BillingContext billingContext;

        public GoodRepository(BillingContext billingContext)
        {
            this.billingContext = billingContext;
        }

        public Result<Good> Create(Good goodToCreate)
        {
            var goodDbo = new GoodDbo
            {
                QuantityType = goodToCreate.QuantityType,
                UnitPrice = goodToCreate.UnitPrice,
                Description = goodToCreate.Description
            };

            var goodComponentLinks = goodToCreate.Components.Select(c => new GoodComponentLinkDbo
            {
                GoodId = goodDbo.Id,
                ComponentId = c.Id,
                Quantity = c.Quantity
            }).ToList();

            goodDbo.GoodComponents = goodComponentLinks;
            billingContext.Goods.Add(goodDbo);
            var createdRows = billingContext.SaveChanges();

            if (createdRows > 0)
            {
                goodToCreate.Id = goodDbo.Id;
                return new Result<Good>
                {
                    IsSuccess = true,
                    Message = "Good successfully created!",
                    Value = goodToCreate
                };
            }

            return new Result<Good>
            {
                IsSuccess = false,
                Message = "Good not created!"
            };
        }

        public Result<Good> Delete(Guid goodId)
        {
            var goodDbo = billingContext.Goods.FirstOrDefault(g => g.Id == goodId);
            if (goodDbo == null)
            {
                return new Result<Good>
                {
                    IsSuccess = false,
                    Message = $"Good with id {goodId} not found!"
                };
            }

            var deletedGood = new Good
            {
                Id = goodDbo.Id,
                QuantityType = goodDbo.QuantityType,
                UnitPrice = goodDbo.UnitPrice,
                Description = goodDbo.Description,
                Components = goodDbo.GoodComponents.Select(item => new GoodComponent
                {
                    Id = item.ComponentId,
                    Quantity = item.Quantity
                }).ToList()
            };

            billingContext.Goods.Remove(goodDbo);
            var deletedRows = billingContext.SaveChanges();

            if (deletedRows > 0)
            {
                return new Result<Good>
                {
                    IsSuccess = true,
                    Message = "Good successfully deleted!",
                    Value = deletedGood
                };
            }

            return new Result<Good>
            {
                IsSuccess = false,
                Message = "Good not deleted!"
            };
        }

        public Result<Good> Get(Guid goodId)
        {
            var goodDbo = billingContext.Goods.FirstOrDefault(g => g.Id == goodId);
            if (goodDbo == null)
            {
                return new Result<Good>
                {
                    IsSuccess = false,
                    Message = $"Good with id {goodId} not found!"
                };
            }

            var good = new Good
            {
                Id = goodDbo.Id,
                QuantityType = goodDbo.QuantityType,
                UnitPrice = goodDbo.UnitPrice,
                Description = goodDbo.Description,
                Components = goodDbo.GoodComponents.Select(item => new GoodComponent
                {
                    Id = item.ComponentId,
                    Quantity = item.Quantity
                }).ToList()
            };

            return new Result<Good>
            {
                IsSuccess = false,
                Message = $"Good with id {goodId} successfully found!",
                Value = good
            };
        }

        public Result<List<Good>> Get()
        {
            var listOfGoods = billingContext.Goods.Select(g => new Good 
            { 
                Id = g.Id,
                QuantityType = g.QuantityType,
                UnitPrice = g.UnitPrice,
                Description = g.Description,
                Components = g.GoodComponents.Select(c => new GoodComponent 
                { 
                    Id = c.ComponentId,
                    Quantity = c.Quantity
                }).ToList()
            }).ToList();

            return new Result<List<Good>>
            {
                IsSuccess = true,
                Message = "List of goods successfully found!",
                Value = listOfGoods
            };
        }

        public Result<Good> Update(Good goodToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
