using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AquaHelps.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AquaHelps.Infrastructure.Authentication;
public class TokenProvider
{
    private readonly IConfiguration _configuration;

    public TokenProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public JwtSecurityToken GetToken(ApplicationUser user, IdentityRole? userRole = null)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetRequiredSection("Jwt:SecurityKey").Get<string>() ?? ""));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserName ?? ""),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim(ClaimTypes.Role, userRole?.Name ?? ""),
        };
        var token = new JwtSecurityToken(
            issuer: string.Join(';', _configuration.GetRequiredSection("Jwt:Issuers").Get<IEnumerable<string>>()),
            audience: string.Join(';', _configuration.GetRequiredSection("Jwt:Audiences").Get<IEnumerable<string>>()),
            claims: claims,
            expires: DateTime.UtcNow.AddDays(_configuration.GetRequiredSection("Jwt:ExpiryInDays").Get<int>()),
            signingCredentials: credentials);

        return token;
    }
}
