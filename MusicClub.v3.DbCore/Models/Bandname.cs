using MusicClub.v3.DbCore.SourceGeneratorAttributes;

namespace MusicClub.v3.DbCore.Models
{
    [GenerateIModelMappers(nameof(Created), nameof(Updated))]
    public class Bandname : IBandname
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }


        public int? ImageId { get; set; }
        public Image? Image { get; set; }

        public required int BandId { get; set; }
        public Band? Band { get; set; }

        public IList<Performance> Performances { get; set; } = [];
    }
}