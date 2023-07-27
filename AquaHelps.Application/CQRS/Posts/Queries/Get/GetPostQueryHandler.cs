using AquaHelps.Application.Validation;
using AquaHelps.Infrastructure;
using AquaHelps.Infrastructure.Repository;

namespace AquaHelps.Application.CQRS.Posts.Queries.Get;
public class GetPostQueryHandler : IRequestHandler<GetPostQuery, OneOf<Post, ErrorCollection>>
{
    private readonly IRepository<Post> _repository;
    private readonly IValidator<GetPostQuery> _validator;

    public GetPostQueryHandler(IRepository<Post> repository, IValidator<GetPostQuery> validator)
    {
        _repository = repository;
        _validator = validator;
    }
    public async Task<OneOf<Post, ErrorCollection>> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        if (!_validator.ValidateTo(request, out var errors, cancellationToken))
            return errors;

        var post = await _repository.GetByIdRequired(request.Id, cancellationToken);

        return post;
    }
}
