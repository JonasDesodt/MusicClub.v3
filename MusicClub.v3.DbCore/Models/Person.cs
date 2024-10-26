using MusicClub.v3.DbCore.SourceGeneratorAttributes;

namespace MusicClub.v3.DbCore.Models
{
    [GenerateIModelMappers("Created", "Updated")]
    public class Person : IPerson
    {
        public int Id { get; set; }

        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }

        public required string Firstname { get; set; }
        public required string Lastname { get; set; }

        public IList<Artist> Artists { get; set; } = [];
    }
}
