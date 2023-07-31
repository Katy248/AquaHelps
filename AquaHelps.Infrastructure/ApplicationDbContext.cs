using AquaHelps.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AquaHelps.Infrastructure;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<DbFile> Files { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Post> Posts { get; set; }

}
