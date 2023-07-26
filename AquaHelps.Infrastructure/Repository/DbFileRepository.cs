using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquaHelps.Domain.Models;
using AquaHelps.Infrastructure.Repositories;

namespace AquaHelps.Infrastructure.Repository;
public class DbFileRepository : Repository<DbFile>
{
    public DbFileRepository(ApplicationDbContext context) : base(context)
    {
    }
}
