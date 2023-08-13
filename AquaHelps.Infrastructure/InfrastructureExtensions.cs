using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AquaHelps.Domain.Models;
using AquaHelps.Infrastructure.Authentication;
using AquaHelps.Infrastructure.Repository;
using AquaHelps.Infrastructure.Setup;
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
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        /*services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();*/
        

        services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new()
                {
                    ValidIssuers = configuration.GetRequiredSection("Jwt:Issuers").Get<IEnumerable<string>>(),
                    ValidAudiences = configuration.GetRequiredSection("Jwt:Audiences").Get<IEnumerable<string>>(),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetRequiredSection("Jwt:SecurityKey").Get<string>() ?? "")),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetRequiredSection("Jwt:SecurityKey").Get<string>() ?? "")),
                };
            });
        services
            .AddTransient<UsersSetup>()
            .AddScoped<TokenProvider>()
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
