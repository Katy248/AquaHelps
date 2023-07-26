using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquaHelps.Application.Validation;
using AquaHelps.Domain.Models;

namespace AquaHelps.Application.CQRS.Posts.Queries.Get;
public record GetPostQuery(string Id) : IRequest<OneOf<Post, ErrorCollection>>;
