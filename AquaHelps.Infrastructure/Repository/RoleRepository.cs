using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AquaHelps.Infrastructure.Repository;
public class RoleRepository : RepositoryBase<IdentityRole>
{
    public RoleRepository(ApplicationDbContext context) : base(context)
    {
    }
    public async Task<IdentityRole?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var role = await GetQuery().FirstOrDefaultAsync(role => role.Name == name, cancellationToken);

        return role;
    }
}
