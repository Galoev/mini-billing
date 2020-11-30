using Microsoft.AspNetCore.Mvc;
using Billing.WebApi.Models;

namespace Billing.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimpleController : ControllerBase
    {
        // GET: api/simple
        [HttpGet]
        public ActionResult<MessageDTO> GetMessage(string name)
        {
            return new MessageDTO { message = $"Hello, {name}!" };
        }
    }
}
