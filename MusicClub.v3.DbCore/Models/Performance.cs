using MusicClub.v3.DbCore.SourceGeneratorAttributes;

namespace MusicClub.v3.DbCore.Models
{
    [GenerateIModelMappers(nameof(Created), nameof(Updated))]
    public class Performance : IPerformance
    {
        public int Id { get; set; }
        public required string Instrument { get; set; }

        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }

        public int? ImageId { get; set; }
        public Image? Image { get; set; }

        public required int ArtistId { get; set; }
        public Artist? Artist { get; set; }

        public required int ActId { get; set; }
        public Act? Act { get; set; }

        public int? BandnameId { get; set; }
        public Bandname? Bandname { get; set; }
    }
}