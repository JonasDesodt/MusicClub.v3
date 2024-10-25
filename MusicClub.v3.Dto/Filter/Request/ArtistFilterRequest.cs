using MusicClub.v3.Dto.SourceGeneratorAttributes;

namespace MusicClub.v3.Dto.Filter.Request
{
    [GenerateFilterResponse]
    [GenerateFilterMappers]
    public class ArtistFilterRequest
    {
        public string? Alias { get; set; }

        public int? PersonId { get; set; }
    }
}
