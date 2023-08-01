using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AquaHelps.Client.Auth;

public class AHAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;

    public AHAuthenticationStateProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var status = await GetStatus();

        var claimsIdentity = new ClaimsIdentity();

        if (status.Authorized)
        {
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, status.UserName));
        }
        else
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Anonymous, true.ToString()));

        
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        return new AuthenticationState(claimsPrincipal);
    }
    private async Task<AccountStatus> GetStatus()
    {
        var status = (await _httpClient.GetFromJsonAsync<AccountStatus>("/api/Account/Status"));

        if (status is null)
            throw new NullReferenceException();

        return status;
    }
}
public record AccountStatus(bool Authorized, string? UserName);