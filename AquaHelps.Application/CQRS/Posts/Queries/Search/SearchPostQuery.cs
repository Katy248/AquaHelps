using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaHelps.Application.CQRS.Posts.Queries.Search;
public record SearchPostQuery(string SearchQuery) : IRequest<IEnumerable<Post>>;
