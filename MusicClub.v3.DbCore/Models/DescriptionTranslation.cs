using MusicClub.v3.DbCore.SourceGeneratorAttributes;

namespace MusicClub.v3.DbCore.Models
{
    [GenerateIModelMappers(nameof(Created), nameof(Updated), nameof(TenantId))]
    public class DescriptionTranslation : IDescriptionTranslation
    {
        public int Id { get; set; }
        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }
        public required int TenantId { get; set; }
        public Tenant? Tenant { get; set; }

        public required string Text { get; set; }

        public required int LanguageId { get; set; }
        public Language? Language { get; set; }

        public required int DescriptionId { get; set; }
        public Description? Description { get; set; }
    }
}
