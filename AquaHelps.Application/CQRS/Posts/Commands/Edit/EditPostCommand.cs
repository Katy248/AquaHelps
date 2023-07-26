using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AquaHelps.Application.Validation;
using AquaHelps.Domain.Models;

namespace AquaHelps.Application.CQRS.Posts.Commands.Edit;
public record EditPostCommand(string Id, string Text, ClaimsPrincipal User) : IRequest<OneOf<Post, ErrorCollection>>;
