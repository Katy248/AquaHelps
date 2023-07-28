using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquaHelps.Application.Validation;
using AquaHelps.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AquaHelps.Application.CQRS.Posts.Queries.Search;
public class SearchPostQueryHandler : IRequestHandler<SearchPostQuery, IEnumerable<Post>>
{
    private readonly ISearchableRepository<Post> _repository;
    private readonly IValidator<SearchPostQuery> _validator;

    public SearchPostQueryHandler(ISearchableRepository<Post> repository, IValidator<SearchPostQuery> validator)
    {
        _repository = repository;
        _validator = validator;
    }
    public async Task<IEnumerable<Post>> Handle(SearchPostQuery request, CancellationToken cancellationToken)
    {
        if(!_validator.ValidateTo(request, out var errors, cancellationToken))
            return Array.Empty<Post>();

        var searchResult = await _repository.Search(request.SearchQuery).ToArrayAsync(cancellationToken);

        return searchResult;
    }
}
