using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaHelps.Domain;
public abstract class DbEntity<TKey>
{
    [Key]
    public TKey Id { get; set; }
}
public abstract class DbEntity : DbEntity<string>
{
    public DbEntity()
    {
        Id = Guid.NewGuid().ToString();
    }
}
