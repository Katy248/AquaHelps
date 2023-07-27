using AquaHelps.Domain.Models;
using AquaHelps.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;

namespace AquaHelps.Infrastructure.Setup;
public class UsersSetup
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleRepository _roleRepository;

    public UsersSetup(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleRepository roleRepository)
    {
        _context = context;
        _userManager = userManager;
        _roleRepository = roleRepository;
    }
    public async Task Setup()
    {
        if ((await _roleRepository.GetByNameAsync(AdminRole.Name)) is null)
        {
            await _roleRepository.Create(AdminRole);
        }

        if ((await _userManager.FindByEmailAsync(AdminUser.Email)) is null)
        {
            await _userManager.CreateAsync(AdminUser, AdminPassword);
            await _userManager.AddToRoleAsync(AdminUser, AdminRole.Name);
        }

    }

    private static readonly ApplicationUser AdminUser = new ApplicationUser
    {
        Email = "admin@admin.admin",
        EmailConfirmed = true,
        UserName = "admin",
    };
    private const string AdminPassword = "P@$$w0rd";
    private static readonly IdentityRole AdminRole = new IdentityRole("ADMIN")
    {

    };
}
