using MusicClub.v3.Dto.SourceGeneratorAttributes;

namespace MusicClub.v3.Dto.Data.Request
{
    [GenerateDataResponse]
    [GenerateDataMappers]
    public class ArtistDataRequest
    {
        public string? Alias { get; set; }

        public required int PersonId { get; set; }
    }
}
