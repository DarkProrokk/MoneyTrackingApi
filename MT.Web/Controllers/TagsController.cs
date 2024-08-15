using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MT.Application.JwtService.Interfaces;
using MT.Application.TagsService.Interfaces;
using MT.Application.UserService.Interfaces;
using MT.Domain.Entity;
using MT.Infrastructure.Utilities;

namespace MT.Web.Controllers;
[Route("api/tags/")]
public class TagsController(ITagsService tagsService, IUserService userService): ControllerBase
{
    
    [Route("add")]
    [Authorize]
    public async Task<IActionResult> AddTag([FromBody] Tag tag)
    {
        var jwt = Tools.GetCookieValue(Request.Cookies, "accessToken");
        if (jwt is null) return Forbid();
        var user = await userService.GetActiveUserFromJwt(jwt);
        if (user == null) return NotFound("GUID not found or invalid in token");
        
        await tagsService.AddWithUserAsync(tag, user);
        return Ok();
    }

    [Route("my")]
    [Authorize]
    public async Task<IActionResult> GetMy()
    {
        var jwt = Tools.GetCookieValue(Request.Cookies, "accessToken");
        if (jwt is null) return Forbid();
        var user = await userService.GetActiveUserFromJwt(jwt);
        if (user == null ) return NotFound("GUID not found or invalid in token");
        var tags = tagsService.GetByUser(user);
        return Ok(tags);
    }
}