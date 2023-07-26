using AquaHelps.Application.Interfaces.CQRS.Queries;
using AquaHelps.Domain.Models;

namespace AquaHelps.Application.CQRS.Posts.Queries.GetAll;
public record GetAllPostsQuery(OrderType OrderType) : IGetListQuery, IRequest<IEnumerable<Post>>;