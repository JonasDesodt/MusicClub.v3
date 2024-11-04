using MusicClub.v3.DbCore.SourceGeneratorAttributes;

namespace MusicClub.v3.DbCore.Models
{
    [GenerateIModelMappers(nameof(Created), nameof(Updated), nameof(TenantId))]
    public class Function : IFunction
    {
        public int Id { get; set; }
        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }
        public required int TenantId { get; set; }
        public Tenant? Tenant { get; set; }

        public required string Name { get; set; }

        public IList<Job> Jobs { get; set; } = [];



        public IList<Service> Services { get; set; } = [];
    }

    //public enum Function {
    //    Bar,
    //    BuildUp,
    //    CleanUp,
    //    Entrance,
    //    Lightning,
    //    Other
    //    Sound
    //}
}
