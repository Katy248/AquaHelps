using AquaHelps.Application.Validation;
using AquaHelps.Infrastructure;

namespace AquaHelps.Application.CQRS.Posts.Queries.Get;
public class GetPostQueryValidator : AbstractValidator<GetPostQuery>
{
    public GetPostQueryValidator(IRepository<Post> repository)
    {
        RuleFor(query => query.Id)
            .NotEmpty()
            .MustAsync(repository.EntityExists);
    }
}
