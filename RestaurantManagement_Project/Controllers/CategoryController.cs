﻿using Application.CQRS.Categories.Command.Requests;
using Application.CQRS.Categories.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagement_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequest request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetByIdCategoryRequest() { Id = id };
            return Ok(await _sender.Send(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllCategoryRequest request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteCategoryRequest() { Id = id };
            return Ok(await _sender.Send(request));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoryRequest request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpGet("getByName")]
        public async Task<IActionResult> GetByName([FromQuery]string name)
        {
            var request = new GetByNameCategoryRequest() { Name = name };
            return Ok(await _sender.Send(request));
        }

    }
}
