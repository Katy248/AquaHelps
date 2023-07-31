using AquaHelps.Application.CQRS.Posts.Models;
using AquaHelps.Application.Validation;
using AquaHelps.Infrastructure;
using AquaHelps.Infrastructure.Repository;
using AutoMapper;

namespace AquaHelps.Application.CQRS.Posts.Queries.Get;
public class GetPostQueryHandler : IRequestHandler<GetPostQuery, OneOf<PostDto, ErrorCollection>>
{
    private readonly IRepository<Post> _repository;
    private readonly IValidator<GetPostQuery> _validator;
    private readonly IMapper _mapper;

    public GetPostQueryHandler(IRepository<Post> repository, IValidator<GetPostQuery> validator, IMapper mapper)
    {
        _repository = repository;
        _validator = validator;
        _mapper = mapper;
    }
    public async Task<OneOf<PostDto, ErrorCollection>> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        if (!_validator.ValidateTo(request, out var errors, cancellationToken))
            return errors;

        var post = await _repository.GetByIdRequired(request.Id, cancellationToken);

        return _mapper.Map<PostDto>(post);
    }
}
