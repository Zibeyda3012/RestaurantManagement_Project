using Application.CQRS.Cars.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagement_Project.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> Add([FromBody]AddCar.AddCarCommandRequest request)
    {
        return Ok(await _sender.Send(request)); 
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var request = new GetById.GetByIdQuery { Id = id };
        return Ok(await _sender.Send(request));
    }

    [HttpGet]
    public async Task<IActionResult> GetByPrice([FromQuery] GetPrice.GetPriceQueryRequest request)
    {
        return Ok(await _sender.Send(request)); 
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var request = new Delete.DeleteCommandRequest { Id = id };
        return Ok(await _sender.Send(request));
    }


}
