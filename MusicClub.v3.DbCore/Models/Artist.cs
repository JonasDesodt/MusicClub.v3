using MusicClub.v3.DbCore.SourceGeneratorAttributes;

namespace MusicClub.v3.DbCore.Models
{
    [GenerateIModelMappers("Created", "Updated")]
    public class Artist : IArtist
    {
        public int Id { get; set; }

        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }

        public required string? Alias { get; set; }

        public required int PersonId { get; set; }
        public Person? Person { get; set; }
    }
}
