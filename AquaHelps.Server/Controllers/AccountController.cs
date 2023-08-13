using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AquaHelps.Domain.Models;
using AquaHelps.Infrastructure.Authentication;
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
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly TokenProvider _tokenProvider;
    private readonly IConfiguration _configuration;

    public AccountController(
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager, 
        RoleManager<IdentityRole> roleManager,
        TokenProvider tokenProvider,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _tokenProvider = tokenProvider;
        _configuration = configuration;
    }
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is null)
            return NotFound();

        //var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
        var result = await _userManager.CheckPasswordAsync(user, model.Password);

        if (result)
        {
            var token = _tokenProvider.GetToken(user);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new LoginResponse(true, tokenString));
        }
        else
            return BadRequest(new LoginResponse(false, null));
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
    [HttpGet("Logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return Ok();
    }
    [HttpGet("Status")]
    public async Task<IActionResult> Status()
    {
        if (_signInManager.IsSignedIn(User))
            return Ok(new AccountStatus(true, _userManager.GetUserName(User), _userManager.GetUserId(User)));
        else
            return Ok(new AccountStatus(false , null, null));
    }
}
