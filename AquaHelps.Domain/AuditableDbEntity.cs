using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquaHelps.Domain.Models;

namespace AquaHelps.Domain;
public abstract class AuditableDbEntity : DbEntity
{
    public string CreatorId { get; set; }
    public ApplicationUser Creator { get; set; }
    public DateTime CreatedOn { get; set; }
}
