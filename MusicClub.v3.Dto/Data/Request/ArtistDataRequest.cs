using MusicClub.v3.Dto.SourceGeneratorAttributes;
using MusicClub.v3.IModels;

namespace MusicClub.v3.Dto.Data.Request
{
    [GenerateDataResponse]
    [GenerateDataMappers]
    public class ArtistDataRequest : IArtist
    {
        public string? Alias { get; set; }

        public required int PersonId { get; set; }
    }
}
