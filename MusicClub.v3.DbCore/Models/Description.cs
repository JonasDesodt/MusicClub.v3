using MusicClub.v3.DbCore.SourceGeneratorAttributes;
using MusicClub.v3.IModels;

namespace MusicClub.v3.DbCore.Models
{
    [GenerateIModelMappers(nameof(Created), nameof(Updated), nameof(TenantId))]
    public class Description : IDescription
    {
        public int Id { get; set; }
        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }
        public required int TenantId { get; set; }
        public Tenant? Tenant { get; set; }

        public IList<DescriptionTranslation> DescriptionTranslations { get; set; } = [];

        public IList<Act> Acts { get; set; } = [];
    }
}
