using CourseSalesAPI.Application.Feautures.Commands.AppUSer.AssignRoleToUser;
using CourseSalesAPI.Application.Feautures.Commands.AppUSer.CreateUser;
using CourseSalesAPI.Application.Feautures.Commands.AppUSer.GoogleLogin;
using CourseSalesAPI.Application.Feautures.Commands.AppUSer.LoginUser;
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
    public class UsersController : ControllerBase
    {

        readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }

        [HttpPost("assign-role-to-user")]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommandRequest assignRoleToUserCommandRequest)
        {
            AssignRoleToUserCommandResponse response = await _mediator.Send(assignRoleToUserCommandRequest);
            return Ok(response);
        }


        



    }
}
