using AquaHelps.Application.Validation;
using Microsoft.AspNetCore.Identity;

namespace AquaHelps.Application.CQRS.Posts.Commands.Create;
public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator(UserManager<ApplicationUser> userManager)
    {
        RuleFor(command => command.Text)
            .NotEmpty();
        RuleFor(command => command.User)
            .MustAsync(userManager.UserExists);
    }
}
