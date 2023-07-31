using AquaHelps.Application.CQRS.Posts.Models;
using AquaHelps.Application.Validation;

namespace AquaHelps.Application.CQRS.Posts.Queries.Get;
public record GetPostQuery(string Id) : IRequest<OneOf<PostDto, ErrorCollection>>;
