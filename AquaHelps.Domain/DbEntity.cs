using System.ComponentModel.DataAnnotations;

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
