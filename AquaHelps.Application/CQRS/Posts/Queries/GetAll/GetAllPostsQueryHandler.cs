using AquaHelps.Application.Validation;
using AquaHelps.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AquaHelps.Application.CQRS.Posts.Queries.GetAll;
internal class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, IEnumerable<Post>>
{
    private readonly IRepository<Post> _repository;
    private readonly IValidator<GetAllPostsQuery> _validator;

    public GetAllPostsQueryHandler(IRepository<Post> repository, IValidator<GetAllPostsQuery> validator)
    {
        _repository = repository;
        _validator = validator;
    }
    public async Task<IEnumerable<Post>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        if (!_validator.ValidateTo(request, out _, cancellationToken))
            return Enumerable.Empty<Post>();

        return await _repository.GetAll().ToArrayAsync(cancellationToken);
    }
}
