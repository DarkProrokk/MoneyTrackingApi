using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MT.Application.ItemsService;
using MT.Application.ItemsService.Interfaces;
using MT.Application.ItemsService.Models;
using MT.Application.UserService.Interfaces;
using MT.Infrastructure.Utilities;

namespace MT.Web.Controllers;
[ApiController]
[Route("api/[controller]/")]
public class ItemsController(IItemService itemService, IUserService userService) : ControllerBase
{
     #region Get

    [HttpGet]
    [Route("all")]
    [Authorize]
    public IActionResult All()
    {
        return Ok(itemService.GetAllItems());
    }

    [HttpGet]
    [Route("byTag/{id:guid}")]
    public IActionResult GetItemByTagId(Guid id)
    {
        return Ok(itemService.GetItemsByTagId(id));
    }

    [HttpGet]
    [Route("byUser/{id}")]
    public IActionResult GetItemByUserId(Guid id)
    {
        return Ok(itemService.GetItemsByUserId(id));
    }

    [HttpGet]
    [Route("my")]
    [Authorize]
    public async Task<IActionResult> GetMy()
    {
        var jwt = Tools.GetCookieValue(Request.Cookies, "accessToken");
        if (jwt is null) return Forbid();
        var user = await userService.GetActiveUserFromJwt(jwt);
        if (user == null) return NotFound("GUID not found or invalid in token");
        var items = itemService.GetItemsByUser(user);
        return Ok(items);
    }

    #endregion

    [HttpPost]
    [Route("add")]
    [Authorize]
    public async Task<IActionResult> AddItem([FromBody] ItemsAddDto item)
    {
        var jwt = Tools.GetCookieValue(Request.Cookies, "accessToken");
        if (jwt is null) return Forbid();
        var user = await userService.GetActiveUserFromJwt(jwt);
        if (user == null) return NotFound("GUID not found or invalid in token");
        await itemService.AddWithUserAsync(item, user);
        return Ok();
    }

    [HttpGet]
    [Route("allTest")]
    public async Task<IActionResult> GetAll()
    {
        var items = itemService.GetAll();
        return Ok(items);
    }
    // [HttpGet]
    // public IActionResult GetByNameAndUseFull([FromQuery] string name, [FromQuery] bool usefull = true)
    // {
    //     return Ok(itemService.GetItemByName(name, usefull));
    // }
}