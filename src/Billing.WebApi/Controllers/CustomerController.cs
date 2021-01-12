using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Billing.WebApi.Repositories;
using Billing.WebApi.Client.Models;
using Billing.WebApi.Client.Utility;
using System.Linq;
using Billing.WebApi.Models;

namespace Billing.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomersRepository customersRepository;

        public CustomerController(ICustomersRepository customersRepository)
        {
            this.customersRepository = customersRepository;
        }

        [HttpGet]
        public ActionResult<Result<List<CustomerDto>>> GetAll()
        {
            var resultFromRepository = customersRepository.Get();
            return new Result<List<CustomerDto>>
            {
                IsSuccess = resultFromRepository.IsSuccess,
                Message = resultFromRepository.Message,
                Value = resultFromRepository.Value != null
                    ? resultFromRepository.Value.Select(c => new CustomerDto
                    {
                        Id = c.Id,
                        Phone = c.Phone,
                        Name = c.Name,
                        AdditionalInfo = c.AdditionalInfo
                    }).ToList()
                    : null
            };
        }

        [HttpGet("{id}")]
        public ActionResult<Result<CustomerDto>> Get(Guid id)
        {
            var resultFromRepository = customersRepository.Get(id);
            return new Result<CustomerDto>
            {
                IsSuccess = resultFromRepository.IsSuccess,
                Message = resultFromRepository.Message,
                Value = resultFromRepository.Value != null
                    ? new CustomerDto 
                    { 
                        Id = resultFromRepository.Value.Id,
                        Name = resultFromRepository.Value.Name,
                        Phone = resultFromRepository.Value.Phone,
                        AdditionalInfo = resultFromRepository.Value.AdditionalInfo
                    } 
                    : null
            };
        }

        [HttpPost]
        public ActionResult<Result<CustomerDto>> Post([FromBody] CustomerDto customerDto)
        {
            var customerToPost = new Customer
            {
                Phone = customerDto.Phone,
                Name = customerDto.Name,
                AdditionalInfo = customerDto.AdditionalInfo
            };

            var resultFromRepository = customersRepository.Create(customerToPost);

            return CreatedAtAction(nameof(Get), new { id = resultFromRepository.Value.Id }, new Result<CustomerDto>
            {
                IsSuccess = resultFromRepository.IsSuccess,
                Message = resultFromRepository.Message,
                Value = resultFromRepository.Value != null
                    ? new CustomerDto
                    {
                        Id = resultFromRepository.Value.Id,
                        Name = resultFromRepository.Value.Name,
                        Phone = resultFromRepository.Value.Phone,
                        AdditionalInfo = resultFromRepository.Value.AdditionalInfo
                    }
                    : null
            });
        }

        [HttpPut]
        public ActionResult<Result<CustomerDto>> Put([FromBody] CustomerDto customerDto)
        {
            var customerToUpdate = new Customer
            {
                Id = customerDto.Id,
                Phone = customerDto.Phone,
                Name = customerDto.Name,
                AdditionalInfo = customerDto.AdditionalInfo
            };
            var resultFromRepository = customersRepository.Update(customerToUpdate);
            return new Result<CustomerDto>
            {
                IsSuccess = resultFromRepository.IsSuccess,
                Message = resultFromRepository.Message,
                Value = resultFromRepository.Value != null
                    ? new CustomerDto
                    {
                        Id = resultFromRepository.Value.Id,
                        Name = resultFromRepository.Value.Name,
                        Phone = resultFromRepository.Value.Phone,
                        AdditionalInfo = resultFromRepository.Value.AdditionalInfo
                    }
                    : null
            };
        }

        [HttpDelete("{id}")]
        public ActionResult<Result<CustomerDto>> Delete(Guid id)
        {
            var resultFromRepository = customersRepository.Delete(id);
            return new Result<CustomerDto>
            {
                IsSuccess = resultFromRepository.IsSuccess,
                Message = resultFromRepository.Message,
                Value = resultFromRepository.Value != null
                    ? new CustomerDto
                    {
                        Id = resultFromRepository.Value.Id,
                        Name = resultFromRepository.Value.Name,
                        Phone = resultFromRepository.Value.Phone,
                        AdditionalInfo = resultFromRepository.Value.AdditionalInfo
                    }
                    : null
            };
        }
    }
}
