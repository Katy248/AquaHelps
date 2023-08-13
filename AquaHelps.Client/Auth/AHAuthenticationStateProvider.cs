using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using AquaHelps.Client.Extensions;
using AquaHelps.Shared.Requests.Account;
using AquaHelps.Shared.Responses.Account;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AquaHelps.Client.Auth;

public class AHAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    public const string LocalStorageTokenKey = "JwtToken";
    public AHAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }
    public async Task Login(string email, string password)
    {
        var request = new LoginRequest(email, password);
        var response = await _httpClient.PostTo<LoginRequest, LoginResponse>("/api/Account/Login", request);
        if (response.Success)
        {
            await _localStorage.SetItemAsStringAsync(LocalStorageTokenKey, response.Token);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
    public async Task Logout()
    {
        await _localStorage.SetItemAsStringAsync(LocalStorageTokenKey, "");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("","");
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsStringAsync(LocalStorageTokenKey);

        if (string.IsNullOrWhiteSpace(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var jwt = GetJwtSecurityToken(token);

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(authenticationType: "apiauth", claims: jwt.Claims)));
    }
    private void GetCookie()
    {
        
    }
    private JwtSecurityToken GetJwtSecurityToken(string jwtToken)
    {
        var token = new JwtSecurityTokenHandler() { MapInboundClaims = true }.ReadJwtToken(jwtToken);
        
        return token;
    }
    public async Task<IEnumerable<Claim>> Claims()
    {
        var item = await _localStorage.GetItemAsStringAsync(LocalStorageTokenKey);
        var token = new JwtSecurityTokenHandler() { MapInboundClaims = true }.ReadJwtToken(item);
        return token.Claims;
    }
    public async Task<JwtSecurityToken> Token()
    {
        var item = await _localStorage.GetItemAsStringAsync(LocalStorageTokenKey);
        var token = new JwtSecurityTokenHandler() { MapInboundClaims = true }.ReadJwtToken(item);
        return token;
    }
    public async Task<bool> IsSignedIn()
    {
        return !string.IsNullOrWhiteSpace(await _localStorage.GetItemAsStringAsync(LocalStorageTokenKey));
    }
    /*private async Task<AccountStatus> GetStatus()
    {
        var status = await _httpClient.GetFromJsonAsync<AccountStatus>("/api/Account/Status");

        if (status is null)
            throw new NullReferenceException("Bad response from server.");

        return status;
    }*/
        /*var status = await GetAuthenticationStateAsync();
        if (status.User.Claims.Any())
        {
            Console.WriteLine(true);
            return true;
        }
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        return false;*/
}
public record AccountStatus(bool Authorized, string? UserName, string? UserId);
/*var status = await GetStatus();

        var claimsIdentity = new ClaimsIdentity(authenticationType: "apiauth");

        if (status.Authorized)
        {
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, status.UserName));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            //claimsIdentity.AddClaim(new Claim(ClaimTypes.Anonymous, false.ToString()));
            //claimsIdentity.AddClaim(new Claim(ClaimTypes.Authentication, true.ToString()));
        }
        else
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        }

        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        return new AuthenticationState(claimsPrincipal);*/