using Application.CQRS.Users.Handlers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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


        [HttpPost("regsiter")]
        [AllowAnonymous]

        public async Task<IActionResult> Regsiter([FromBody] Register.Command request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpDelete]
        public async Task<IActionResult> Remove([FromQuery] int id)
        {
            var request = new Delete.Command() { Id = id };
            return Ok(await _sender.Send(request));
        }

        //[HttpPut("update")]

        //public async Task<IActionResult> Update([FromBody] Update.Command request)
        //{
        //    return Ok(await _sender.Send(request));
        //}

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Application.CQRS.Users.Handlers.Login.LoginRequest request)
        {
            return(Ok(await _sender.Send(request)));    
        }

    }
}
