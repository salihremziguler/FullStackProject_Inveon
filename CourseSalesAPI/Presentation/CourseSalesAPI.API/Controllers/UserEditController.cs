using CourseSalesAPI.Application.Feautures.Commands.AppUSer.UpdateUser;
using CourseSalesAPI.Domain.Entities.Identity;
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

        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommandRequest updateUserRequest, [FromServices] UserManager<AppUser> userManager, [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var username = httpContextAccessor.HttpContext?.User?.Identity?.Name;
            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized(new { message = "Token geçerli bir kullanıcı içermiyor." });
            }

            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound(new { message = "Kullanıcı bulunamadı." });
            }

            user.NameSurname = updateUserRequest.NameSurname ?? user.NameSurname;

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok(new { message = "Kullanıcı bilgileri güncellendi." }); // JSON formatında yanıt
            }

            return BadRequest(new { message = "Kullanıcı bilgileri güncellenemedi." });
        }

    }
}
