using MusicClub.v3.DbCore.SourceGeneratorAttributes;

namespace MusicClub.v3.DbCore.Models
{
    [GenerateIModelMappers(nameof(Created), nameof(Updated))]
    public class GoogleEvent : IGoogleEvent
    {
        public int Id { get; set; }
            
        public required string GoogleIdentifier { get; set; }

        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }

        public required int GoogleCalendarId { get; set; }
        public GoogleCalendar? GoogleCalendar { get; set; }

        public Act? Act { get; set; }
    }
}
