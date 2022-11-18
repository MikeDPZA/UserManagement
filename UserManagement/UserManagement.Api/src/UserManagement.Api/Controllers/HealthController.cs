using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserManagement.Api.Controllers;

/// <summary>
/// Health Checks for UserManagement
/// </summary>
[ApiController]
[Route("api/UserManagement/v1/Health")]
public class HealthController: Controller
{
    /// <summary>
    /// Returns Pong
    /// </summary>
    /// <returns>
    /// A 200 OK response with the body "Pong"
    /// </returns>
    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok("Pong");
    }
    
    /// <summary>
    /// If the user is authenticated, return a 200 OK response with the text "Pong"
    /// </summary>
    /// <returns>
    /// A 200 OK response with the body "Pong"
    /// </returns>
    [Authorize]
    [HttpGet("authorized")]
    public IActionResult AuthorizedEndpoint()
    {
        return Ok("Pong");
    }
}