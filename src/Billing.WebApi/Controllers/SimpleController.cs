using Microsoft.AspNetCore.Mvc;
using Billing.WebApi.Models;
using Billing.WebApi.DAL;
using System.Collections.Generic;

namespace Billing.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimpleController : ControllerBase
    {
        private readonly IStorage storage;

        public SimpleController(IStorage storage)
        {
            this.storage = storage;
        }

        // GET: api/simple
        [HttpGet]
        public ActionResult<MessageDTO> GetMessage(string name)
        {
            return new MessageDTO { message = $"Hello, {name}!" };
        }

        [HttpGet]
        [Route("order")]
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            return storage.Orders.Get();
        }

        [HttpGet]
        [Route("order/{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = storage.Orders.Get(id);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

        [HttpPost]
        [Route("order")]
        public ActionResult<Order> PostOrder(Order orderToPost)
        {
            storage.Orders.Add(orderToPost);
            storage.Commit();
            return CreatedAtAction(nameof(GetOrder), new { id = orderToPost.ID}, orderToPost);
        }
    }
}
