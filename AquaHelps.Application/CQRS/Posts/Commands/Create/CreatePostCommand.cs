using System.Security.Claims;
using AquaHelps.Application.Validation;

namespace AquaHelps.Application.CQRS.Posts.Commands.Create;

public record CreatePostCommand(string Text, ClaimsPrincipal User) : IRequest<OneOf<Post, ErrorCollection>>;
