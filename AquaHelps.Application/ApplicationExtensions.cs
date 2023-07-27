global using AquaHelps.Domain.Models;
global using FluentValidation;
global using MediatR;
global using OneOf;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AquaHelps.Application;
public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddValidatorsFromAssembly(typeof(ApplicationExtensions).Assembly)
            .AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
    }
}
