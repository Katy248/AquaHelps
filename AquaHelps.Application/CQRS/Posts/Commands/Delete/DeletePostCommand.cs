using System.Security.Claims;
using AquaHelps.Application.Validation;

namespace AquaHelps.Application.CQRS.Posts.Commands.Delete;
public record DeletePostCommand(string Id, ClaimsPrincipal User) : IRequest<OneOf<bool, ErrorCollection>>;
