using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaHelps.Domain.Models;
public class Post : AuditableDbEntity
{
    public string Text { get; set; }
}
