using MusicClub.v3.DbCore.SourceGeneratorAttributes;

namespace MusicClub.v3.DbCore.Models
{
    [GenerateIModelMappers(nameof(Created), nameof(Updated))]
    public class Artist : IArtist
    {
        public int Id { get; set; }

        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }

        public required string? Alias { get; set; }

        public required int PersonId { get; set; }
        public Person? Person { get; set; }

        public int? ImageId { get; set; }
        public Image? Image { get; set; }

        public IList<Performance> Performances { get; set; } = [];
    }
}
