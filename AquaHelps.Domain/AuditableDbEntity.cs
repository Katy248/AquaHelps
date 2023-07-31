using AquaHelps.Domain.Models;

namespace AquaHelps.Domain;
public abstract class AuditableDbEntity : DbEntity
{
    public ApplicationUser Creator { get; set; }
    public string CreatorId { get; set; }
    public DateTime CreatedOn { get; set; }
}
