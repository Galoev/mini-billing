using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Billing.WebApi.Repositories;
using Billing.WebApi.Models;
using Billing.WebApi.Client.Models;

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
        public ActionResult<IEnumerable<OrderDto>> Get()
        {
            return ordersRepository.Get().ConvertAll(new Converter<Order, OrderDto>(OrderToDto));
        }

        // GET api/<BillingController>/5
        [HttpGet("{id}")]
        public ActionResult<OrderDto> Get(int id)
        {
            return OrderToDto(ordersRepository.Get(id));
        }

        // POST api/<BillingController>
        [HttpPost]
        public ActionResult<OrderDto> Post([FromBody] OrderDto orderDto)
        {
            var orderToPost = DtoToOrder(orderDto);
            ordersRepository.Create(orderToPost);
            return CreatedAtAction(nameof(Get), new { id = orderToPost.Id }, orderToPost);
        }

        // PUT api/<BillingController>
        [HttpPut]
        public void Put([FromBody] OrderDto orderDto)
        {
            ordersRepository.Update(DtoToOrder(orderDto));
        }

        // DELETE api/<BillingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ordersRepository.Delete(id);
        }
    }
}
