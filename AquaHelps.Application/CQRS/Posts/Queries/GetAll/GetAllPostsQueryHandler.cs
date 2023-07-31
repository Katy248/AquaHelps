using AquaHelps.Application.CQRS.Posts.Models;
using AquaHelps.Application.Validation;
using AquaHelps.Infrastructure;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AquaHelps.Application.CQRS.Posts.Queries.GetAll;
internal class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, IEnumerable<PostDto>>
{
    private readonly IRepository<Post> _repository;
    private readonly IValidator<GetAllPostsQuery> _validator;
    private readonly IMapper _mapper;

    public GetAllPostsQueryHandler(IRepository<Post> repository, IValidator<GetAllPostsQuery> validator, IMapper mapper)
    {
        _repository = repository;
        _validator = validator;
        _mapper = mapper;
    }
    public async Task<IEnumerable<PostDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        if (!_validator.ValidateTo(request, out _, cancellationToken))
            return Enumerable.Empty<PostDto>();

        return await 
            _mapper.ProjectTo<PostDto>(_repository.GetAll()).ToArrayAsync(cancellationToken);
    }
}
