using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AquaHelps.Application.Validation;

namespace AquaHelps.Application.CQRS.Posts.Commands.Delete;
public record DeletePostCommand(string Id, ClaimsPrincipal User) : IRequest<OneOf<bool, ErrorCollection>>;
