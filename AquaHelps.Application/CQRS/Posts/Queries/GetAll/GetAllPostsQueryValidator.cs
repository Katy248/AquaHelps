namespace AquaHelps.Application.CQRS.Posts.Queries.GetAll;
public class GetAllPostsQueryValidator : AbstractValidator<GetAllPostsQuery>
{
    public GetAllPostsQueryValidator()
    {
        RuleFor(query => query.OrderType)
            .NotEmpty();
    }
}
