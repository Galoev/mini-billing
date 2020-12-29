using Microsoft.VisualStudio.TestTools.UnitTesting;
using Billing.WebApi.Client.Models;
using Billing.WebApi.Models.Converter;
using System;
using System.Collections.Generic;

namespace Billing.WebApi.Tests.Models.Converter
{
    [TestClass]
    public class GoodConverterTests
    {
        [TestMethod]
        public void FromCreateDto_ReturnModelToGetDtoCanConvert()
        {
            var componentGoodGuid = Guid.NewGuid();
            var createGoodDto = new CreateGoodDto
            {
                QuantityType = QuantityType.Kilogram,
                UnitPrice = 1000.0M,
                Description = "Very juicy meat",
                Components = new List<GoodComponentDto>
                {
                    new GoodComponentDto{ Id = componentGoodGuid, Quantity = 12 }
                }
            };

            var goodConverter = new GoodConverter();
            var good = goodConverter.FromCreateDto(createGoodDto);
            var getGoodDto = goodConverter.ToGetDto(good);
            Assert.AreEqual(getGoodDto.QuantityType, createGoodDto.QuantityType);
            Assert.AreEqual(getGoodDto.UnitPrice, createGoodDto.UnitPrice);
            Assert.AreEqual(getGoodDto.Description, createGoodDto.Description);
            Assert.AreEqual(getGoodDto.Components[0].Quantity, createGoodDto.Components[0].Quantity);
            Assert.AreEqual(getGoodDto.Components[0].Id, createGoodDto.Components[0].Id);
        }
    }
}
