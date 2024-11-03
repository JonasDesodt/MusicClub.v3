namespace MusicClub.v3.DbCore.Models
{
    public class DescriptionTranslation : IDescriptionTranslation
    {
        public int Id { get; set; }
        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }

        public required string Text { get; set; }

        public required int LanguageId { get; set; }
        public Language? Language { get; set; }

        public required int DescriptionId { get; set; }
        public Description? Description { get; set; }
    }
}
