using AquaHelps.Application.Validation;
using AquaHelps.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace AquaHelps.Application.CQRS.Posts.Commands.Edit;
public class EditPostCommandValidator : AbstractValidator<EditPostCommand>
{
    public EditPostCommandValidator(IRepository<Post> repository, UserManager<ApplicationUser> userManager)
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .MustAsync(repository.EntityExists);
        RuleFor(command => command.User)
            .NotEmpty()
            .MustAsync(userManager.UserExists);
        RuleFor(command => command.Text)
            .NotEmpty();
    }
}
