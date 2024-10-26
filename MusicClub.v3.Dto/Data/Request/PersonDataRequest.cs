using MusicClub.v3.Dto.SourceGeneratorAttributes;
using MusicClub.v3.IModels;

namespace MusicClub.v3.Dto.Data.Request
{
    [GenerateDataResponse]
    [GenerateDataMappers]
    public class PersonDataRequest : IPerson
    {
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
    }
}
