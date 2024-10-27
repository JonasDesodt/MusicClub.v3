using MusicClub.v3.Dto.Enums;
using MusicClub.v3.Dto.SourceGeneratorAttributes;
using MusicClub.v3.IModels;
using System.ComponentModel;

namespace MusicClub.v3.Dto.Filter.Request
{
    [GenerateFilterRequest(typeof(IArtist))]
    [GenerateFilterResponse(typeof(IArtist))]
    [GenerateFilterMappers(typeof(IArtist))]
    [GenerateFilterRequestExtensions(typeof(IArtist))]
    public partial class ArtistFilterRequest 
    {
        public string? SortProperty { get; set; }

        //[DefaultValue(SortDirection.Ascending)]
        public SortDirection? SortDirection { get; set; }
    }
}
