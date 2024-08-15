using BizLogics.Models;
using Microsoft.AspNetCore.Mvc;
using MT.Application.UserService.Interfaces;
using MT.Domain.Entity;

namespace MT.Web.Controllers;

[ApiController]
[Route("api/auth/")]
public class AuthenticationController(IUserService service): ControllerBase
{
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterUser(UserRegisterModel model)
    {
        var user = new User
        {
            Id = model.Id,
            Email = model.Email,
            UserName = model.Login
        };
        if (!await service.IsExistAsync(user.Id))
        {
            return BadRequest("User already exists");
        }
        await service.AddAsync(user);
        return Ok();
    }
}