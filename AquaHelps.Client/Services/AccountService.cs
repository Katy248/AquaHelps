using System.Net.Http.Json;

namespace AquaHelps.Client.Services;

public class AccountService
{
    private readonly HttpClient _client;

    public AccountService(HttpClient client)
    {
        _client = client;
    }
    public async Task<bool> IsSignedIn()
    {
        var response = await _client.GetFromJsonAsync<AccountStatus>("/api/Account/Status");

        return response?.Status == "SignedIn";
    }
}
public record class AccountStatus(string Status);
