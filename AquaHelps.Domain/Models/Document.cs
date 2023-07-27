namespace AquaHelps.Domain.Models;
public class Document : AuditableDbEntity
{
    public string Name { get; set; }
    public string FileId { get; set; }
    public DbFile File { get; set; }
}
