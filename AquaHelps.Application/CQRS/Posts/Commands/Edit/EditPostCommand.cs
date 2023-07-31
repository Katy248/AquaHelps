using System.Security.Claims;
using AquaHelps.Application.CQRS.Posts.Models;
using AquaHelps.Application.Validation;

namespace AquaHelps.Application.CQRS.Posts.Commands.Edit;
public record EditPostCommand(string Id, string Text, ClaimsPrincipal User) : IRequest<OneOf<PostDto, ErrorCollection>>;
