using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaHelps.Application.CQRS.Posts.Queries.Search;
public class SearchPostQueryValidator : AbstractValidator<SearchPostQuery>
{
    public SearchPostQueryValidator()
    {
        RuleFor(query => query.SearchQuery)
            .NotEmpty();
    }
}
