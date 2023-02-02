using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using UserManagement.Api.Extensions;
using UserManagement.Services.Attributes;
using UserManagement.Services.Services;

namespace UserManagement.Services.Middleware;

public class PermissionMiddleware
{
    private readonly RequestDelegate _next;

    public PermissionMiddleware(RequestDelegate next)
    {
        _next = next;
    }


    /// <summary>
    /// If the endpoint has a `PermissionRequiredAttribute` then check if the user has the required permissions. If not,
    /// throw a `ForbiddenException`
    /// </summary>
    /// <param name="context">The HttpContext object for the current request.</param>
    /// <param name="currentUser">Class that contains the token user's details</param>
    public async Task InvokeAsync(HttpContext context, ICurrentUser currentUser)
    {
        var endpoint = context.GetEndpoint();
        var attribute = endpoint?.Metadata.GetMetadata<PermissionAttribute>();
        if (attribute != null && !currentUser.HasPermission(attribute.Permissions))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;

            return;
        }

        await _next(context);
    }
}