using MusicClub.v3.DbCore.SourceGeneratorAttributes;
using MusicClub.v3.IModels;

namespace MusicClub.v3.DbCore.Models
{
    [GenerateIModelMappers(nameof(Created), nameof(Updated))]
    public class Act : IAct
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Title { get; set; }
        public DateTime? Start { get; set; } 
        public int? Duration { get; set; }


        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }


        public int? ImageId { get; set; }
        public Image? Image { get; set; }

        public IList<Performance> Performances { get; set; } = [];

        public IList<Job> Jobs { get; set; } = [];


        public required int LineupId { get; set; }
        public Lineup? Lineup { get; set; }


        public int? GoogleEventId { get; set; }
        public GoogleEvent? GoogleEvent { get; set; }

        public int? DescriptionId { get; set; }
        public Description? Description { get; set; }
    }
}