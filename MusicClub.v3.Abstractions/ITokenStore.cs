namespace MusicClub.v3.Abstractions
{
    public interface ITokenStore
    {
        Task<string> GetAccessToken();

        Task RemoveToken();
    }
}
