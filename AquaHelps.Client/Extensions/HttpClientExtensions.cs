using System.Net.Http.Json;
using AquaHelps.Client.Auth;
using Blazored.LocalStorage;

namespace AquaHelps.Client.Extensions;

public static class HttpClientExtensions
{
    public static async Task<TResponse?> PostTo<TRequest, TResponse>(this HttpClient client, string uri, TRequest request)
    {
        var result = await client.PostAsJsonAsync(uri, request);
        var response = await result.Content.ReadFromJsonAsync<TResponse>();
        return response;
    }
    public static IServiceCollection AddHttpClient(this IServiceCollection services, Uri baseAddress)
    {
        return services
            .AddScoped<HttpClient>(sp =>
            {
                var localStorage = sp.GetRequiredService<ILocalStorageService>();
                var jwtToken = localStorage.GetItemAsStringAsync(AHAuthenticationStateProvider.LocalStorageTokenKey).GetAwaiter().GetResult();
                var client = new HttpClient { BaseAddress = baseAddress };
                if (!string.IsNullOrWhiteSpace(jwtToken))
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", $"Bearer {jwtToken}");

                return client;
            });
    }
}
