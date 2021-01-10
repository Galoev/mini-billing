using Billing.WebApi.Client.Models;
using Billing.WebApi.Models;
using Billing.WebApi.Repositories;
using Billing.WebApi.Client.Utility;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Billing.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentsController : ControllerBase
    {
        private readonly IComponentRepository componentRepository;

        public ComponentsController(IComponentRepository componentRepository)
        {
            this.componentRepository = componentRepository;
        }

        [HttpGet("{id}")]
        public ActionResult<Result<GetComponentDto>> Get(Guid id)
        {
            var resultFromRepository = componentRepository.Get(id);
            return new Result<GetComponentDto>
            {
                IsSuccess = resultFromRepository.IsSuccess,
                Message = resultFromRepository.Message,
                Value = resultFromRepository.Value != null
                    ? new GetComponentDto
                    {
                        Id = resultFromRepository.Value.Id,
                        QuantityType = resultFromRepository.Value.QuantityType,
                        UnitPrice = resultFromRepository.Value.UnitPrice,
                        Description = resultFromRepository.Value.Description
                    }
                    : null
            };
        }

        [HttpPost()]
        public ActionResult<Result<GetComponentDto>> Post([FromBody] CreateComponentDto createComponentDto)
        {
            var componentToCreate = new Component
            {
                QuantityType = createComponentDto.QuantityType,
                UnitPrice = createComponentDto.UnitPrice,
                Description = createComponentDto.Description
            };

            var resultFromRepository = componentRepository.Create(componentToCreate);
            return CreatedAtAction(nameof(Get), new { id = resultFromRepository.Value.Id }, new Result<GetComponentDto>
            {
                IsSuccess = resultFromRepository.IsSuccess,
                Message = resultFromRepository.Message,
                Value = resultFromRepository.Value != null
        ? new GetComponentDto
        {
            Id = resultFromRepository.Value.Id,
            QuantityType = resultFromRepository.Value.QuantityType,
            UnitPrice = resultFromRepository.Value.UnitPrice,
            Description = resultFromRepository.Value.Description
        }
        : null
            });
        }

        [HttpDelete("{id}")]
        public ActionResult<Result<GetComponentDto>> Delete(Guid id)
        {
            var resultFromRepository = componentRepository.Delete(id);
            return new Result<GetComponentDto>
            {
                IsSuccess = resultFromRepository.IsSuccess,
                Message = resultFromRepository.Message,
                Value = resultFromRepository.Value != null
                    ? new GetComponentDto
                    {
                        Id = resultFromRepository.Value.Id,
                        QuantityType = resultFromRepository.Value.QuantityType,
                        UnitPrice = resultFromRepository.Value.UnitPrice,
                        Description = resultFromRepository.Value.Description
                    }
                    : null
            };
        }
    }
}
