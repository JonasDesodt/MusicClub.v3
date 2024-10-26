using MusicClub.v3.DbCore.SourceGeneratorAttributes;

namespace MusicClub.v3.DbCore.Models
{
    //[GenerateDataRequestMappers(excludeProperties: ["Id", "Person"])]
    public class Artist
    {
        public int Id { get; set; }

        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }

        public required string Alias { get; set; }

        public required int PersonId { get; set; }
        public Person? Person { get; set; }
    }
}
