using Microsoft.AspNetCore.Mvc;
using UserManagement.Common.Dto.Permission;
using UserManagement.Services.Interfaces;
using UserManagement.Services.Services;

namespace UserManagement.Api.Controllers;

/// <summary>
/// Controller for permission related actions
/// </summary>
[Route("api/UserManagement/v1/Permissions")]
public class PermissionController: BaseController
{
    private readonly IPermissionControllerService _permissionControllerService;

    /// <summary>
    /// Default Constructor for Permission Controller
    /// </summary>
    /// <param name="currentUser"></param>
    public PermissionController(ICurrentUser currentUser, IPermissionControllerService permissionControllerService) : base(currentUser)
    {
        _permissionControllerService = permissionControllerService;
    }

    /// <summary>
    /// Get Permissions
    /// </summary>
    /// <param name="pageNum">The current page number</param>
    /// <param name="pageSize">The amount of items to return</param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetPermissions([FromQuery] int pageNum = 1, [FromQuery] int pageSize = 10)
    {
        var result = _permissionControllerService.GetPermissions(pageNum, pageSize);
        return Ok(result);
    }
    
    /// <summary>
    /// Search Permissions
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("Search")]
    public IActionResult SearchPermissions([FromBody] PermissionFilterDto? filter)
    {
        var result = _permissionControllerService.GetPermissions(filter.PageNum, filter.PageSize,filter);
        return Ok(result);
    }
}