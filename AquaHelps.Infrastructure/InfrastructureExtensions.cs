using AquaHelps.Domain.Models;
using AquaHelps.Infrastructure.Repository;
using AquaHelps.Infrastructure.Setup;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AquaHelps.Infrastructure;
public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.
            AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        services
            .AddScoped<IRepository<Document>, DocumentRepository>()
            .AddScoped<IRepository<Post>, PostRepository>();

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
        })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
            ;

        services.AddAuthentication()
            .AddIdentityServerJwt();


        services
            .AddTransient<UsersSetup>()
            .AddScoped<RoleRepository>();

        return services;
    }
    public static WebApplication SetupDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        scope.ServiceProvider.GetRequiredService<UsersSetup>().Setup()
            .GetAwaiter().GetResult();

        return app;
    }
}
