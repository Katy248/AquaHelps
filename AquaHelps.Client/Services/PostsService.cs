using System.Net.Http.Json;
using System.Web;
using AquaHelps.Domain.Models;

namespace AquaHelps.Client.Services;

public class PostsService
{
    private readonly HttpClient _client;

    public PostsService(HttpClient client)
    {
        _client = client;
    }
    public async Task<IEnumerable<Post>> GetPosts()
    {
        var posts = await _client.GetFromJsonAsync<IEnumerable<Post>>("/api/Posts/Get");
        return posts;
    }
    public async Task<IEnumerable<Post>> Search(string searchQuery)
    {
        var posts = await _client.GetFromJsonAsync<IEnumerable<Post>>($"/api/Posts/Search/{HttpUtility.HtmlEncode(searchQuery)}");
        return posts;
    }
}
