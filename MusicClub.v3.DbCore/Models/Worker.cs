using MusicClub.v3.DbCore.SourceGeneratorAttributes;

namespace MusicClub.v3.DbCore.Models
{
    [GenerateIModelMappers(nameof(Created), nameof(Updated), nameof(TenantId))]
    public class Worker : IWorker
    {
        public int Id { get; set; }
        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }
        public required int TenantId { get; set; }
        public Tenant? Tenant { get; set; }
        
        public required bool IsTechnician { get; set; }
        public required bool IsEmployee { get; set; }

        public required int PersonId { get; set; }
        public Person? Person { get; set; }

        public IList<Job> Jobs { get; set; } = [];

        public IList<Service> Services { get; set; } = [];
    }
}