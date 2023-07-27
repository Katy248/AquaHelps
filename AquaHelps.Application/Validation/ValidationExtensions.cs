using System.Security.Claims;
using AquaHelps.Domain;
using AquaHelps.Infrastructure;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AquaHelps.Application.Validation;
public static class ValidationExtensions
{
    /// <summary>
    /// Validate object and return result of validation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="validator"></param>
    /// <param name="obj">Validation object.</param>
    /// <param name="errors">Sequence of <see cref="ValidationFailure"/>. If validation success sets to <see cref="ErrorCollection.Empty"/>.</param>
    /// <returns>Result of validation. <see cref="true"/> if object is valid, otherwise <see cref="false"/>.</returns>
    public static bool ValidateTo<T>(this IValidator<T> validator, T obj, out ErrorCollection errors, CancellationToken cancellationToken = default)
    {
        errors = ErrorCollection.Empty;
        var validationResult = validator.ValidateAsync(obj, cancellationToken).GetAwaiter().GetResult();

        if (validationResult.IsValid)
            return true;

        errors = new(validationResult.Errors);

        return false;
    }





    public static async Task<bool> UserExists(this UserManager<ApplicationUser> userManager, ClaimsPrincipal user, CancellationToken cancellationToken = default)
    {
        return (await userManager.Users.AsNoTracking().FirstOrDefaultAsync(dbUser => dbUser.Id == userManager.GetUserId(user))) is not null;
    }
    public static async Task<bool> EntityExists<TEntity>(this IRepository<TEntity> repository, string id, CancellationToken cancellationToken = default) where TEntity : DbEntity
    {
        return (await repository.GetById(id, cancellationToken)) is not null;
    }
}
