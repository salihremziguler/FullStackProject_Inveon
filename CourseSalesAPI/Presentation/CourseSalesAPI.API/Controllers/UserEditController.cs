using CourseSalesAPI.Application.Feautures.Commands.AppUSer.CreateUser;
using CourseSalesAPI.Application.Feautures.Commands.AppUSer.DeleteUser;
using CourseSalesAPI.Application.Feautures.Commands.AppUSer.UpdateUser;
using CourseSalesAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseSalesAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class UserEditController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UserEditController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }

        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommandRequest updateUserRequest)
        {
           UpdateUserCommandResponse response= await _mediator.Send(updateUserRequest);
            return Ok(response);


        }

        [HttpDelete("delete-user/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var request = new DeleteUserCommandRequest { UserId = userId };
            var response = await _mediator.Send(request);
            return Ok(response);
        }

    }
}
