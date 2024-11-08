using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MusicClub.v3.Abstractions;
using MusicClub.v3.Abstractions.Services;
using MusicClub.v3.ApiServices;
using MusicClub.v3.Cms;
using MusicClub.v3.Cms.Handlers;
using MusicClub.v3.Cms.Providers;
using MusicClub.v3.Cms.Requirements;
using MusicClub.v3.Cms.Services;
using MusicClub.v3.Cms.Stores;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Helpers;
using MusicClubManager.Blazor.Handlers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<AuthorizationHttpHandler>();
builder.Services.AddHttpClient("MusicClubApi", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7023");
}).AddHttpMessageHandler<AuthorizationHttpHandler>();

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

builder.Services.AddScoped<IActService, ActApiService>();

builder.Services.AddScoped<IFilterRequestHelpers<ActFilterRequest, ActFilterResponse>, ActFilterRequestHelpers>();

builder.Services.AddTransient<JsFunctions>();

await builder.Build().RunAsync();
