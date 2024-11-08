using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MusicClub.v3.Abstractions;
using MusicClub.v3.ApiServices;
using MusicClub.v3.Cms;
using MusicClub.v3.Cms.Handlers;
using MusicClub.v3.Cms.Providers;
using MusicClub.v3.Cms.Requirements;
using MusicClub.v3.Cms.Services;
using MusicClub.v3.Cms.Stores;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient("MusicClubApi", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7023");
});//.AddHttpMessageHandler<AuthorizationHttpHandler>();

builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<ITokenStore, TokenStore>();

builder.Services.AddScoped<AuthApiService>();
     
builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("ValidTokenOnly", policy => policy.Requirements.Add(new ValidTokenRequirement()));

});
builder.Services.AddScoped<IAuthorizationHandler, ValidTokenHandler>();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddCascadingAuthenticationState();

await builder.Build().RunAsync();