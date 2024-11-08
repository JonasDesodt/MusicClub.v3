using System.Net.Http.Json;
using MusicClub.v3.Dto.Auth.Request;
using MusicClub.v3.Dto.Auth.Response;

namespace MusicClub.v3.ApiServices
{
    public class AuthApiService(IHttpClientFactory httpClientFactory)
    {
        public async Task<TokenAuthResponse?> GetToken(TokenAuthRequest tokenAuthRequest)
        {
            var httpClient = httpClientFactory.CreateClient("MusicClubApi");

            var httpResponseMessage = await httpClient.PostAsJsonAsync("private/auth/token", tokenAuthRequest);

            if (!httpResponseMessage.IsSuccessStatusCode || await httpResponseMessage.Content.ReadFromJsonAsync<TokenAuthResponse>() is not { } tokenAuthResponse)
            {
                return null;
            }

            return tokenAuthResponse;
        }
    }
}
