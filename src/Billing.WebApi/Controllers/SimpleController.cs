using Microsoft.AspNetCore.Mvc;
using Billing.WebApi.Client.Models;
using Billing.WebApi.Models;
using Billing.WebApi.DAL;
using System.Collections.Generic;

namespace Billing.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimpleController : ControllerBase
    {

        // GET: api/simple
        [HttpGet]
        public ActionResult<MessageDto> GetMessage(string name)
        {
            return new MessageDto { message = $"Hello, {name}!" };
        }

    }
}
