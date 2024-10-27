using MusicClub.v3.DbCore.SourceGeneratorAttributes;

namespace MusicClub.v3.DbCore.Models
{
    [GenerateIModelMappers(nameof(Created), nameof(Updated))]
    public class GoogleCalendar : IGoogleCalendar
    {
        public int Id { get; set; }

        public required string GoogleIdentifier { get; set; }

        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }

        public IList<GoogleEvent> GoogleEvents { get; set; } = [];
    }
}
