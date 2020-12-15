using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Billing.WebApi.Repositories;
using Billing.WebApi.Models;
using Billing.WebApi.Client.Models;
using Billing.WebApi.Models.Converter;

namespace Billing.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly IOrdersRepository ordersRepository;
        private readonly IOrderConverter orderConverter;

        public BillingController(IOrdersRepository ordersRepository, IOrderConverter orderConverter)
        {
            this.ordersRepository = ordersRepository;
            this.orderConverter = orderConverter;
        }

        // GET: api/<BillingController>
        [HttpGet]
        public ActionResult<IEnumerable<GetOrderDto>> Get()
        {
            throw new NotImplementedException();
        }

        // GET api/<BillingController>/5
        [HttpGet("{id}")]
        public ActionResult<GetOrderDto> Get(int id)
        {
            return orderConverter.ToGetDto(ordersRepository.Get(id));
        }

        // POST api/<BillingController>
        [HttpPost]
        public ActionResult<CreateOrderDto> Post([FromBody] CreateOrderDto orderDto)
        {
            var orderToPost = orderConverter.FromCreateDto(orderDto);
            ordersRepository.Create(orderToPost);
            return CreatedAtAction(nameof(Get), new { id = orderToPost.Id }, orderToPost);
        }

        // PUT api/<BillingController>
        [HttpPut]
        public void Put([FromBody] CreateOrderDto orderDto)
        {
            ordersRepository.Update(orderConverter.FromCreateDto(orderDto));
        }

        // DELETE api/<BillingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ordersRepository.Delete(id);
        }
    }
}
