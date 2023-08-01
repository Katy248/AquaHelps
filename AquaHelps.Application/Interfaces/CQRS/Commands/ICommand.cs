using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquaHelps.Application.Validation;

namespace AquaHelps.Application.Interfaces.CQRS.Commands;
public interface ICommand<TResponse> : IRequest<OneOf<TResponse, ErrorCollection>>
{
}
