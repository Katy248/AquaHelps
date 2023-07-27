using AquaHelps.Domain.Models;
using AquaHelps.Infrastructure.Repositories;

namespace AquaHelps.Infrastructure.Repository;
public class DbFileRepository : Repository<DbFile>
{
    public DbFileRepository(ApplicationDbContext context) : base(context)
    {
    }
}
