using CourseSalesAPI.Application.Feautures.Commands.AppUSer.UpdateUser;
using CourseSalesAPI.Application.Feautures.Commands.Basket.AddItemToBasket;
using CourseSalesAPI.Application.Feautures.Commands.Basket.RemoveBasketItem;
using CourseSalesAPI.Application.Feautures.Queries.Basket.GetBasketItems;
using CourseSalesAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseSalesAPI.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class BasketsController : ControllerBase
    {
        readonly IMediator _mediator;

        public BasketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasketItems([FromQuery] GetBasketItemsQueryRequest getBasketItemsQueryRequest)
        {
            List<GetBasketItemsQueryResponse> response = await _mediator.Send(getBasketItemsQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToBasket(AddItemToBasketCommandRequest addItemToBasketCommandRequest)
        {
            AddItemToBasketCommandResponse response = await _mediator.Send(addItemToBasketCommandRequest);
            return Ok(response);
        }

       
        [HttpDelete("{BasketItemId}")]
        public async Task<IActionResult> RemoveBasketItem([FromRoute] RemoveBasketItemCommandRequest removeBasketItemCommandRequest)
        {
            RemoveBasketItemCommandResponse response = await _mediator.Send(removeBasketItemCommandRequest);
            return Ok(response);
        }


        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommandRequest updateUserRequest, [FromServices] UserManager<AppUser> userManager, [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var username = httpContextAccessor.HttpContext?.User?.Identity?.Name;
            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("Token geçerli bir kullanıcı içermiyor.");
            }

            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            user.NameSurname = updateUserRequest.NameSurname ?? user.NameSurname;

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok("Kullanıcı bilgileri güncellendi.");
            }

            return BadRequest("Kullanıcı bilgileri güncellenemedi.");
        }


    }
}
