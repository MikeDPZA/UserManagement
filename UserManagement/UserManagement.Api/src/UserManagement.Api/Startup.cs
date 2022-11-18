using UserManagement.Services;
using UserManagement.Services.Extensions;

namespace UserManagement.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    /// This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(Configuration);
        services.AddControllers();

        services.AddUserManagementServices(Configuration);
    }

    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();;
        app.UsePermissionMiddleware();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

    }
}