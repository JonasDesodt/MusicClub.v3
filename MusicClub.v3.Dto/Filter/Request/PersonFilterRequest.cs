using MusicClub.v3.Dto.SourceGeneratorAttributes;

namespace MusicClub.v3.Dto.Filter.Request
{
    [GenerateFilterResponse]
    [GenerateFilterMappers]
    public class PersonFilterRequest
    {
        public string? Firstname { get; set; }

        public string? Lastname { get; set; }
    }
}
