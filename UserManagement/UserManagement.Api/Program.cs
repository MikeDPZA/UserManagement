using System.Reflection;
using Microsoft.OpenApi.Models;
using UserManagement.Api.Extensions;
using UserManagement.Api.Services;
using UserManagement.ControllerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "UserManagement",
        Description = "A REST API to manage users in a system"
    });
    
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddScoped<CurrentUser>();

builder.Services.AddControllerServices(builder.Configuration);


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.UsePermissionMiddleware();

app.MapControllers();

app.Run();

public partial class Program { }