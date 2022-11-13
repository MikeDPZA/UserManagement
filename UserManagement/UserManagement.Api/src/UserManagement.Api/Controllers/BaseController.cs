using Microsoft.AspNetCore.Mvc;
using UserManagement.Api.Services;

namespace UserManagement.Api.Controllers;

/// <summary>
/// Base Controller all controllers should inherit from
/// </summary>
// [Authorize("Token")]
[Produces("application/json")]
public abstract class BaseController: ControllerBase
{
    protected CurrentUser CurrentUser;
    
    /// <summary>
    /// Default constructor for base controller
    /// </summary>
    /// <param name="currentUser"></param>
    public BaseController(CurrentUser currentUser)
    {
        CurrentUser = currentUser;
    }
}