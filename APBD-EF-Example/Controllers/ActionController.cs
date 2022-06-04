using System.Net;
using APBD_EF_Example.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_EF_Example.Controllers;

[Route("api/[controller]")]
[Controller]
public class ActionController : ControllerBase
{
    private readonly IDbService _dbService;
    
    public ActionController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("/{idAction:int}")]
    public async Task<IActionResult> GetAction(int idAction)
    {
        var response = await _dbService.GetActionWithFiretrucksAsync(idAction);
        
        if (response.StatusCode != HttpStatusCode.OK)
        {
            return StatusCode((int) response.StatusCode, response.Content);
        }

        return Ok(response.Content);
    }

    [HttpPost("/{idAction:int}/firetrucks")]
    public async Task<IActionResult> AddFiretruckToAction(int idAction, [FromQuery] int idFiretruck)
    {
        var response = await _dbService.AddFiretruckToActionAsync(idAction, idFiretruck);
        
        if (response.StatusCode != HttpStatusCode.OK)
        {
            return StatusCode((int) response.StatusCode, response.Content);
        }

        return Ok(response.Content);
    }
    
}