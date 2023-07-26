using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquaHelps.Application.Validation;
using AquaHelps.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace AquaHelps.Application.CQRS.Posts.Commands.Delete;
public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
{
    public DeletePostCommandValidator(IRepository<Post> repository, UserManager<ApplicationUser> userManager)
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .MustAsync(repository.EntityExists);
        RuleFor(command => command.User)
            .NotEmpty()
            .MustAsync(userManager.UserExists);
    }
}
