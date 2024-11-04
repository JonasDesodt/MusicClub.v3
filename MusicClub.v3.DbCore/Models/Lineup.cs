using MusicClub.v3.DbCore.SourceGeneratorAttributes;

namespace MusicClub.v3.DbCore.Models
{
    [GenerateIModelMappers(nameof(Created), nameof(Updated), nameof(TenantId))]
    public class Lineup : ILineup
    {
        public int Id { get; set; }
        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }
        public required int TenantId { get; set; }
        public Tenant? Tenant { get; set; }

        public string? Title { get; set; }
        public required DateTime Doors { get; set; }

        public int? ImageId { get; set; }
        public Image? Image { get; set; }

        public IList<Act> Acts { get; set; } = [];

        public IList<Service> Services { get; set; } = [];
    }
}