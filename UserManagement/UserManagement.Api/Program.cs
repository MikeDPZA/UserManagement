using System.Net.Mime;
using Microsoft.AspNetCore.Diagnostics;
using UserManagement.Api.Extensions;
using UserManagement.Api.Services;
using UserManagement.Common.Exceptions;
using UserManagement.ControllerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<CurrentUser>();
builder.Services.AddControllerServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UsePermissionMiddleware();

// TODO: Verify working
app.UseExceptionHandler(exceptionHandler =>
{
    exceptionHandler.Run(async context =>
    {

        context.Response.ContentType = MediaTypeNames.Application.Json;
        var handler = context.Features.Get<IExceptionHandlerPathFeature>();

        if (handler?.Error is ForbiddenException)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("User does not have required permission");
            return;
        }
        
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsync("Something went wrong");
    });
});

app.MapControllers();

app.Run();