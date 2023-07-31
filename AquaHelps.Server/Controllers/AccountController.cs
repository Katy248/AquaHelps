using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AquaHelps.Domain.Models;
using AquaHelps.Shared.Requests.Account;
using AquaHelps.Shared.Responses.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AquaHelps.Server.Controllers;

[ApiController, Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is null)
            return NotFound();

        var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

        if (result.Succeeded)
            return Ok();
        else
            return BadRequest();
    }
    [HttpPost("ChangePassword"), Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest model)
    {
        var user = (await _userManager.GetUserAsync(User))!;

        var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

        if (result.Succeeded)
            return Ok();
        else
            return BadRequest(result.Errors);
    }
    [HttpGet("Logout"), Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return Ok();
    }
    [HttpGet("Status")]
    public async Task<IActionResult> Status()
    {
        if (_signInManager.IsSignedIn(User))
            return Ok(new AccountStatus("SignedIn"));
        else
            return Ok(new AccountStatus("NotSignedIn"));
    }
}
