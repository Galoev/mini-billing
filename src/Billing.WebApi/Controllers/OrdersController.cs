using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Billing.WebApi.Repositories;
using Billing.WebApi.Client.Models;
using Billing.WebApi.Client.Utility;
using Billing.WebApi.Models.Converter;
using System.Linq;

namespace Billing.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository ordersRepository;
        private readonly IOrderConverter orderConverter;

        public OrdersController(IOrdersRepository ordersRepository, IOrderConverter orderConverter)
        {
            this.ordersRepository = ordersRepository;
            this.orderConverter = orderConverter;
        }

        [HttpGet]
        public ActionResult<Result<List<GetOrderDto>>> GetAll()
        {
            var resultFromRepository = ordersRepository.Get();
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
            // TODO: добавить получение цены из компонента подсчета цены заказа
            orderToPost.Price = 0.0M;

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
        public ActionResult<Result<GetOrderDto>> Put([FromBody] CreateOrderDto orderDto)
        {
            var resultFromRepository = ordersRepository.Update(orderConverter.FromCreateDto(orderDto));
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
