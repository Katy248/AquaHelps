using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaHelps.Domain.Models;
public class Document : AuditableDbEntity
{
    public string Name { get; set; }
    public string FileId { get; set; }
    public DbFile File { get; set; }
}
