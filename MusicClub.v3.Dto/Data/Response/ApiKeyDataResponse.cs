namespace MusicClub.v3.Dto.Data.Response
{
    public partial class ApiKeyDataResponse
    {
        public DateTime? Archived { get; set; }

        public required string ApiKey { get; set; }
    }
}
