using AquaHelps.Client;
using AquaHelps.Client.Services;
using AquaHelps.Domain.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress/*"https://localhost:7060"*/) });

/*builder.Services.AddIdentityCore<ApplicationUser>(setup =>
{
    
});*/
builder.Services
    .AddScoped<MarkdownService>()
    .AddScoped<AccountService>()
    .AddScoped<PostsService>()
    .AddScoped<DocumentsService>()
    .AddBlazoredLocalStorage()  ;

await builder.Build().RunAsync();
