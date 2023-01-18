using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Services.Services;

namespace UserManagement.Api.Controllers;

/// <summary>
/// Base Controller all controllers should inherit from
/// </summary>
[Authorize]
[Produces("application/json")]
public abstract class BaseController: ControllerBase
{
    /// <summary>
    /// Current user being used
    /// </summary>
    protected ICurrentUser CurrentUser;
    
    /// <summary>
    /// Default constructor for base controller
    /// </summary>
    /// <param name="currentUser"></param>
    public BaseController(ICurrentUser currentUser)
    {
        CurrentUser = currentUser;
    }
}