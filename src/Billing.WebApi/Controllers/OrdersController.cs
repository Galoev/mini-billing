using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Billing.WebApi.Repositories;
using Billing.WebApi.Client.Models;
using Billing.WebApi.Client.Utility;
using Billing.WebApi.Models.Converter;
using System.Linq;
using Billing.WebApi.Services;

namespace Billing.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository ordersRepository;
        private readonly IOrderConverter orderConverter;
        private readonly IOrderPriceCalculator orderPriceCalculator;

        public OrdersController(IOrdersRepository ordersRepository, 
            IOrderConverter orderConverter, IOrderPriceCalculator orderPriceCalculator)
        {
            this.ordersRepository = ordersRepository;
            this.orderConverter = orderConverter;
            this.orderPriceCalculator = orderPriceCalculator;
        }

        [HttpGet]
        public ActionResult<Result<List<GetOrderDto>>> GetAll()
        {
            var resultFromRepository = ordersRepository.GetAll();
            return new Result<List<GetOrderDto>>
            {
                IsSuccess = resultFromRepository.IsSuccess,
                Message = resultFromRepository.Message,
                Value = resultFromRepository.Value != null
                    ? resultFromRepository.Value.Select(o => orderConverter.ToGetDto(o)).ToList()
                    : null
            };
        }

        [HttpGet("{id}")]
        public ActionResult<Result<GetOrderDto>> Get(Guid id)
        {
            var resultFromRepository = ordersRepository.Get(id);
            return new Result<GetOrderDto>
            {
                IsSuccess = resultFromRepository.IsSuccess,
                Message = resultFromRepository.Message,
                Value = resultFromRepository.Value != null 
                    ? orderConverter.ToGetDto(resultFromRepository.Value) 
                    : null
            };
        }

        [HttpPost]
        public ActionResult<Result<GetOrderDto>> Post([FromBody] CreateOrderDto orderDto)
        {
            var orderToPost = orderConverter.FromCreateDto(orderDto);
            orderToPost.Price = orderPriceCalculator.CalculateOrderPrice(orderDto.Goods);

            var resultFromRepository = ordersRepository.Create(orderToPost);

            return CreatedAtAction(nameof(Get), new { id = resultFromRepository.Value.Id }, new Result<GetOrderDto>
            {
                IsSuccess = resultFromRepository.IsSuccess,
                Message = resultFromRepository.Message,
                Value = resultFromRepository.Value != null 
                    ? orderConverter.ToGetDto(resultFromRepository.Value) 
                    : null
            });
        }

        [HttpPut]
        public ActionResult<Result<GetOrderDto>> Put([FromBody] UpdateOrderDto orderDto)
        {
            var resultFromRepository = ordersRepository.Update(orderConverter.FromUpdateDto(orderDto));
            return new Result<GetOrderDto>
            {
                IsSuccess = resultFromRepository.IsSuccess,
                Message = resultFromRepository.Message,
                Value = resultFromRepository.Value != null 
                    ? orderConverter.ToGetDto(resultFromRepository.Value) 
                    : null
            };
        }

        [HttpDelete("{id}")]
        public ActionResult<Result<GetOrderDto>> Delete(Guid id)
        {
            var resultFromRepository = ordersRepository.Delete(id);
            return new Result<GetOrderDto>
            {
                IsSuccess = resultFromRepository.IsSuccess,
                Message = resultFromRepository.Message,
                Value = resultFromRepository.Value != null 
                    ? orderConverter.ToGetDto(resultFromRepository.Value) 
                    : null
            };
        }
    }
}
