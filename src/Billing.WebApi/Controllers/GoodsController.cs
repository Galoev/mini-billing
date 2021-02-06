using Billing.WebApi.Client.Models;
using Billing.WebApi.Models.Converter;
using Billing.WebApi.Repositories;
using Billing.WebApi.Client.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Billing.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private readonly IGoodsRepository goodsRepository;
        private readonly IGoodConverter goodConverter;

        public GoodsController(IGoodsRepository goodsRepository,
                               IGoodConverter goodConverter)
        {
            this.goodConverter = goodConverter;
            this.goodsRepository = goodsRepository;
        }

        [HttpGet]
        public ActionResult<Result<List<GetGoodDto>>> GetAll()
        {
            var resultFromRepository = goodsRepository.Get();
            if (resultFromRepository.IsSuccess)
            {
                var listOfGoodDto = resultFromRepository.Value.ConvertAll(g => goodConverter.ToGetDto(g));
                return new Result<List<GetGoodDto>>
                {
                    IsSuccess = true,
                    Message = resultFromRepository.Message,
                    Value = listOfGoodDto
                };
            }
            return new Result<List<GetGoodDto>>
            {
                IsSuccess = false,
                Message = resultFromRepository.Message
            };
        }

        [HttpGet("{id}")]
        public ActionResult<Result<GetGoodDto>> Get(Guid id)
        {
            var resultFromRepository = goodsRepository.Get(id);
            return new Result<GetGoodDto>
            {
                IsSuccess = resultFromRepository.IsSuccess,
                Message = resultFromRepository.Message,
                Value = resultFromRepository.Value != null
                    ? goodConverter.ToGetDto(resultFromRepository.Value)
                    : null
            };
        }

        [HttpPost]
        public ActionResult<Result<GetGoodDto>> Post([FromBody] CreateGoodDto createGoodDto)
        {
            var goodToPost = goodConverter.FromCreateDto(createGoodDto);

            var resultFromRepository = goodsRepository.Create(goodToPost);

            return CreatedAtAction(nameof(Get), new { id = resultFromRepository.Value.Id }, new Result<GetGoodDto>
            {
                IsSuccess = resultFromRepository.IsSuccess,
                Message = resultFromRepository.Message,
                Value = resultFromRepository.Value != null
                    ? goodConverter.ToGetDto(resultFromRepository.Value)
                    : null
            });
        }

        [HttpPut]
        public ActionResult<Result<GetGoodDto>> Put([FromBody] UpdateGoodDto goodDto)
        {
            var resultFromRepository = goodsRepository.Update(goodConverter.FromUpdateDto(goodDto));
            return new Result<GetGoodDto>
            {
                IsSuccess = resultFromRepository.IsSuccess,
                Message = resultFromRepository.Message,
                Value = resultFromRepository.Value != null
                    ? goodConverter.ToGetDto(resultFromRepository.Value)
                    : null
            };
        }

        [HttpDelete("{id}")]
        public ActionResult<Result<GetGoodDto>> Delete(Guid id)
        {
            var resultFromRepository = goodsRepository.Delete(id);
            return new Result<GetGoodDto>
            {
                IsSuccess = resultFromRepository.IsSuccess,
                Message = resultFromRepository.Message,
                Value = resultFromRepository.Value != null
                    ? goodConverter.ToGetDto(resultFromRepository.Value)
                    : null
            };
        }
    }
}
