using Microsoft.AspNetCore.Authorization;

namespace UserManagement.Api.Controllers;

[Authorize("Token")]
public class BaseController
{
    
}