using MusicClub.v3.DbCore.SourceGeneratorAttributes;

namespace MusicClub.v3.DbCore.Models
{
    [GenerateIModelMappers(nameof(Created), nameof(Updated))]
    public class Lineup : ILineup
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public required DateTime Doors { get; set; }

        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }

        public int? ImageId { get; set; }
        public Image? Image { get; set; }

        public IList<Act> Acts { get; set; } = [];

        public IList<Service> Services { get; set; } = [];
    }
}