using MusicClub.v3.Abstractions;
using MusicClub.v3.Cms.Extensions;
using MusicClub.v3.Cms.Models;
using MusicClub.v3.Cms.Services;

namespace MusicClub.v3.Cms.Stores
{
    internal class TokenStore(LocalStorageService localStorageService) : ITokenStore
    {
        public async Task<string> GetAccessToken()
        {
            var localStorageToken = await localStorageService.GetItem<LocalStorageToken>("Token");

            if (localStorageToken.IsAccessTokenValid() && localStorageToken?.AccessToken is { } accessToken)
            {
                return accessToken;
            }

            return ""; // temp hack, use refresh token

        }

        public async Task RemoveToken()
        {
            await localStorageService.RemoveItem("Token");
        }
    }
}
