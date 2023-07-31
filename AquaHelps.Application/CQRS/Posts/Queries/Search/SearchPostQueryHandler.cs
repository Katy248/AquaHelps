using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquaHelps.Application.CQRS.Posts.Models;
using AquaHelps.Application.Validation;
using AquaHelps.Infrastructure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AquaHelps.Application.CQRS.Posts.Queries.Search;
public class SearchPostQueryHandler : IRequestHandler<SearchPostQuery, IEnumerable<PostDto>>
{
    private readonly ISearchableRepository<Post> _repository;
    private readonly IValidator<SearchPostQuery> _validator;
    private readonly IMapper _mapper;

    public SearchPostQueryHandler(ISearchableRepository<Post> repository, IValidator<SearchPostQuery> validator, IMapper mapper)
    {
        _repository = repository;
        _validator = validator;
        _mapper = mapper;
    }
    public async Task<IEnumerable<PostDto>> Handle(SearchPostQuery request, CancellationToken cancellationToken)
    {
        if(!_validator.ValidateTo(request, out var errors, cancellationToken))
            return Array.Empty<PostDto>();

        var searchResult = _repository.Search(request.SearchQuery);

        return await _mapper.ProjectTo<PostDto>(searchResult).ToArrayAsync(cancellationToken);
    }
}
