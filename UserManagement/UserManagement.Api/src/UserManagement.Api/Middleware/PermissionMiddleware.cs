using Microsoft.AspNetCore.Http.Features;
using UserManagement.Api.Attributes;
using UserManagement.Api.Services;
using UserManagement.Common.Exceptions;

namespace UserManagement.Api.Middleware;

public class PermissionMiddleware
{
     private readonly RequestDelegate _next;

     public PermissionMiddleware(RequestDelegate next)
     {
          _next = next;
     }

     public async Task InvokeAsync(HttpContext context, CurrentUser currentUser)
     {
          var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
          var attribute = endpoint?.Metadata.GetMetadata<PermissionRequiredAttribute>();
          if (attribute != null)
          {
               var permissions = attribute.Permissions;
               if (!currentUser.Permissions.Any(_ => permissions.Contains(_)))
               {
                    throw new ForbiddenException("User does not required have permission");
               }
          }
          
          await _next(context);
     }
}