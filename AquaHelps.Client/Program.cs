using AquaHelps.Client;
using AquaHelps.Client.Auth;
using AquaHelps.Client.Services;
using AquaHelps.Domain.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress/*"https://localhost:7060"*/) });

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AHAuthenticationStateProvider>();

builder.Services
    .AddScoped<MarkdownService>()
    .AddScoped<AccountService>()
    .AddScoped<PostsService>()
    .AddScoped<DocumentsService>();


await builder.Build().RunAsync();
