using System.Security.Claims;
using AquaHelps.Application.CQRS.Posts.Models;
using AquaHelps.Application.Validation;

namespace AquaHelps.Application.CQRS.Posts.Commands.Create;

public record CreatePostCommand(string Text, ClaimsPrincipal User) : IRequest<OneOf<PostDto, ErrorCollection>>;
