using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AquaHelps.Application.Validation;
using AquaHelps.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace AquaHelps.Application.CQRS.Posts.Commands.Create;
public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public CreatePostCommandValidator(UserManager<ApplicationUser> userManager)
    {
        RuleFor(command => command.Text)
            .NotEmpty();
        RuleFor(command => command.User)
            .MustAsync(userManager.UserExists);

        _userManager = userManager;
    }
}
