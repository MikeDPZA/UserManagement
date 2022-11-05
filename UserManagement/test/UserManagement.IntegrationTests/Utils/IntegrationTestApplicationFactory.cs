using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UserManagement.Repository.Context;

namespace UserManagement.IntegrationTests.Utils;

public class IntegrationTestApplicationFactory<TStartup>: WebApplicationFactory<TStartup> where TStartup: class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.FirstOrDefault(_ => _.ServiceType == typeof(DbContextOptions<UserManagementContext>));

            services.Remove(descriptor);

            services.AddDbContext<UserManagementContext>(options =>
            {
                options.UseInMemoryDatabase("UserManagement");
            });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<UserManagementContext>();
            var logger = scopedServices
                .GetRequiredService<ILogger<IntegrationTestApplicationFactory<TStartup>>>();

            db.Database.EnsureCreated();

            try
            {
                UserUtilities.InitializeUsersForTests(db);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the " +
                                    "database with test messages. Error: {Message}", ex.Message);
            }
        });
    }
}