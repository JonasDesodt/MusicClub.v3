namespace MusicClub.v3.DbCore.Models
{
    public class Image : IImage
    {
        public int Id { get; set; }
        public required string Alt { get; set; }
        public required byte[] Content { get; set; }
        public required string ContentType { get; set; }

        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }

        public IList<Artist> Artists { get; set; } = [];
        public IList<Act> Acts { get; set; } = [];
        public IList<Person> People { get; set; } = [];
        public IList<Performance> Performances { get; set; } = [];
        public IList<Lineup> Lineups { get; set; } = [];
    }
}