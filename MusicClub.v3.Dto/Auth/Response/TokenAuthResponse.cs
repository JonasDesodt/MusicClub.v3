namespace MusicClub.v3.Dto.Auth.Response
{
    public class TokenAuthResponse
    {
        public required string TokenType { get; set; }

        public required string AccessToken { get; set; }

        public required int ExpiresIn { get; set; }
    }
}
