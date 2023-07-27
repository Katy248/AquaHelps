using AquaHelps.Application.Interfaces.CQRS.Queries;

namespace AquaHelps.Application.CQRS.Posts.Queries.GetAll;
public record GetAllPostsQuery(OrderType OrderType) : IGetListQuery, IRequest<IEnumerable<Post>>;