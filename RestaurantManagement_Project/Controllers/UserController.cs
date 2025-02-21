using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.CQRS.Users.Handlers.GetById;
using static Application.CQRS.Users.Handlers.Register;

namespace RestaurantManagement_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        [HttpGet]

        public async Task<IActionResult> GetById([FromQuery] Query request)
        {
            return Ok(await _sender.Send(request));
        }


        //// exception ile olan variant ucun
        //[HttpGet]
        //public async Task<IActionResult> GetById([FromQuery] int id)
        //{
        //    var request = new Query() { Id = id };
        //    return Ok(await _sender.Send(request));
        //}


        [HttpPost]
        public async Task<IActionResult> Regsiter([FromBody] Command request)
        {
            return Ok(await _sender.Send(request));
        }
    }
}
