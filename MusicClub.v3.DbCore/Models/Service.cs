using MusicClub.v3.DbCore.SourceGeneratorAttributes;

namespace MusicClub.v3.DbCore.Models
{
    [GenerateIModelMappers(nameof(Created), nameof(Updated))]
    public class Service : IService
    {
        public int Id { get; set; }
        public required string Description { get; set; }

        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }

        public required int WorkerId { get; set; }
        public Worker? Worker { get; set; }

        public required int FunctionId { get; set; }
        public Function? Function { get; set; }

        public required int LineupId { get; set; }
        public Lineup? Lineup { get; set; }
    }
}