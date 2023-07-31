using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquaHelps.Application.CQRS.Posts.Models;

namespace AquaHelps.Application.CQRS.Posts.Queries.Search;
public record SearchPostQuery(string SearchQuery) : IRequest<IEnumerable<PostDto>>;
