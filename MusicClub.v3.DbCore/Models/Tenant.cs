namespace MusicClub.v3.DbCore.Models
{
    public class Tenant
    {
        public int Id { get; set; }

        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }

        public IList<Act> Acts { get; set; } = [];
        public IList<Artist> Artists { get; set; } = [];
        public IList<Band> Bands { get; set; } = [];
        public IList<Bandname> Bandnames { get; set; } = [];
        public IList<Description> Descriptions { get; set; } = [];
        public IList<DescriptionTranslation> DescriptionTranslations { get; set; } = [];
        public IList<Function> Functions { get; set; } = [];
        public IList<GoogleCalendar> GoogleCalendars { get; set; } = [];
        public IList<GoogleEvent> GoogleEvents { get; set; } = [];
        public IList<Image> Images { get; set; } = [];
        public IList<Job> Jobs { get; set; } = [];
        public IList<Lineup> Lineups { get; set; } = [];
        public IList<Performance> Performances { get; set; } = [];
        public IList<Person> People { get; set; } = [];
        public IList<Service> Services { get; set; } = [];
        public IList<Worker> Workers { get; set; } = [];
             
        public required string Name { get; set; }

        public string? ApiKey { get; set; } //to do => set required or not?

        public IList<Tenancy> Tenancies { get; set; } = [];
    }
}
