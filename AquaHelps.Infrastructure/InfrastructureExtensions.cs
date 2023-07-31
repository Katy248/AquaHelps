using System.Text;
using AquaHelps.Domain.Models;
using AquaHelps.Infrastructure.Repository;
using AquaHelps.Infrastructure.Setup;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AquaHelps.Infrastructure;
public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString, IConfiguration configuration)
    {
        services.
            AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        services
            .AddScoped<IRepository<Document>, DocumentRepository>()
            .AddScoped<ISearchableRepository<Post>, PostRepository>()
            .AddScoped<IRepository<Post>, PostRepository>();

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
        })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        /*services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();*/
        
        services.AddAuthorization();

        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            /*.AddCookie(options =>
            {
                options.Cookie.Name = configuration.GetSection("Cookie")["Name"];
                options.ExpireTimeSpan = TimeSpan.Parse(configuration.GetSection("Cookie")["ExpiresAfter"]);
            })
            .AddApplicationCookie()*/;
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
