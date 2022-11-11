using System.Net;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Api.Services;
using UserManagement.Common.Dto.User;
using UserManagement.Common.Generic;
using UserManagement.Services.Interfaces;

namespace UserManagement.Api.Controllers;

/// <summary>
/// Controller for user actions
/// </summary>
[ApiController]
[Route("api/v1/Users")]
public class UserController: BaseController
{
    private IUserControllerService _userControllerService;


    /// <summary>
    /// Constructor for UserController
    /// </summary>
    /// <param name="currentUser"></param>
    /// <param name="userControllerService"></param>
    public UserController(CurrentUser currentUser, IUserControllerService userControllerService) : base(currentUser)
    {
        _userControllerService = userControllerService;
    }

    /// <summary>
    /// Get a list of users from the Database
    /// </summary>
    /// <param name="pageNum">The page number to return.</param>
    /// <param name="pageSize">The number of items to return per page.</param>
    /// <returns>
    /// A list of users.
    /// </returns>
    [ProducesResponseType(typeof(PagedResponse<User>), (int)HttpStatusCode.OK)]
    [HttpGet]
    public IActionResult GetUsers([FromQuery] int pageNum = 1, [FromQuery]int pageSize = 10)
    {
        var result = _userControllerService.GetUsers(pageNum, pageSize);
        return Ok(result);
    }

    /// <summary>
    /// Get a list of users from the Database
    /// </summary>
    /// <param name="filter">The filter to apply to the data set</param>
    /// <param name="pageNum">The page number to return.</param>
    /// <param name="pageSize">The number of items to return per page.</param>
    /// <returns>
    /// A list of users.
    /// </returns>
    [ProducesResponseType(typeof(PagedResponse<User>), (int)HttpStatusCode.OK)]
    [HttpPost("Filter")]
    public IActionResult GetUsers([FromBody]UserFilterDto filter, [FromQuery] int pageNum = 1, [FromQuery]int pageSize = 10)
    {
        var result = _userControllerService.GetUsers(pageNum, pageSize, filter);
        return Ok(result);
    }
}