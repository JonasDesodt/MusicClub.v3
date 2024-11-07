using MusicClub.v3.Cms.Models;

namespace MusicClub.v3.Cms.Extensions
{
    internal static class LocalStorageTokenExtensions
    {
        public static bool IsAccessTokenValid(this LocalStorageToken? localStorageToken)
        {
            if (localStorageToken is null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(localStorageToken.AccessToken))
            {
                return false;
            }

            if (localStorageToken.Received.AddSeconds(localStorageToken.ExpiresIn) <= DateTime.UtcNow.AddSeconds(localStorageToken.ExpiresIn / 2))
            {
                return false;
            }

            return true;
        }
    }
}
