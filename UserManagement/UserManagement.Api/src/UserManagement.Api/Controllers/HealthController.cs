using Microsoft.AspNetCore.Mvc;

namespace UserManagement.Api.Controllers;

/// <summary>
/// Health Checks for UserManagement
/// </summary>
[ApiController]
[Route("api/UserManagement/v1/Health")]
public class HealthController: ControllerBase
{
    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok("Pong");
    }
}