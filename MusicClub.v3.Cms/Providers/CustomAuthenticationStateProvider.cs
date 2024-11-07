using Microsoft.AspNetCore.Components.Authorization;
using MusicClub.v3.Cms.Extensions;
using MusicClub.v3.Cms.Models;
using MusicClub.v3.Cms.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MusicClub.v3.Cms.Providers
{
    internal class CustomAuthenticationStateProvider(LocalStorageService localStorageService) : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var tokens = await localStorageService.GetItem<LocalStorageToken>("Token");

            if (tokens is null || !tokens.IsAccessTokenValid())
            {
                await localStorageService.RemoveItem("Token"); // todo => temp hack, don't remove if token is null

                var notAuthenticatedState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

                NotifyAuthenticationStateChanged(Task.FromResult(notAuthenticatedState));

                return notAuthenticatedState;
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(tokens.AccessToken);

            var claimsPrincipcal = new ClaimsPrincipal(new ClaimsIdentity(jwtToken.Claims, "Jwt"));

            var authenticatedState = new AuthenticationState(claimsPrincipcal);

            NotifyAuthenticationStateChanged(Task.FromResult(authenticatedState));

            return authenticatedState;
        }
    }
}
