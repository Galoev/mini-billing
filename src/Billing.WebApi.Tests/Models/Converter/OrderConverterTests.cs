using Microsoft.VisualStudio.TestTools.UnitTesting;
using Billing.WebApi.Client.Models;
using Billing.WebApi.Models.Converter;
using System;
using System.Collections.Generic;

namespace Billing.WebApi.Tests.Models.Converter
{
    [TestClass]
    public class OrderConverterTests
    {
        [TestMethod]
        public void FromCreateDto_ReturnModelToGetDtoCanConvert()
        {
            var customerGuid = Guid.NewGuid();
            var orderGoodGuid = Guid.NewGuid();
            var createOrderDto = new CreateOrderDto
            {
                CustomerId = customerGuid,
                CreationDate = new DateTime(2020, 12, 20),
                PaymentStatus = PaymentStatus.Paid,
                DeliveryStatus = DeliveryStatus.DeliveryWaiting,
                Goods = new List<OrderGoodDto>
                {
                    new OrderGoodDto{ Id = orderGoodGuid, Quantity = 12 }
                }
            };

            var orderConverter = new OrderConverter();
            var order = orderConverter.FromCreateDto(createOrderDto);
            var getOrderDto = orderConverter.ToGetDto(order);
            Assert.AreEqual(getOrderDto.Customer.Id, createOrderDto.CustomerId);
            Assert.AreEqual(getOrderDto.CreationDate, createOrderDto.CreationDate);
            Assert.AreEqual(getOrderDto.PaymentStatus, createOrderDto.PaymentStatus);
            Assert.AreEqual(getOrderDto.DeliveryStatus, createOrderDto.DeliveryStatus);
        }
    }
}
