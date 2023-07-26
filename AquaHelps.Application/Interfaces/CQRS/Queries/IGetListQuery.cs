using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaHelps.Application.Interfaces.CQRS.Queries;
public interface IGetListQuery
{
    OrderType OrderType { get; }
}
