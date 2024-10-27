using MusicClub.v3.Dto.Enums;
using MusicClub.v3.Dto.SourceGeneratorAttributes;
using MusicClub.v3.IModels;
using System.ComponentModel;

namespace MusicClub.v3.Dto.Filter.Request
{
    [GenerateFilterRequest(typeof(IBand))]
    [GenerateFilterResponse(typeof(IBand))]
    [GenerateFilterMappers(typeof(IBand))]
    [GenerateFilterRequestExtensions(typeof(IBand))]
    public partial class BandFilterRequest 
    {
        public string? SortProperty { get; set; }

        //[DefaultValue(SortDirection.Ascending)]
        public SortDirection? SortDirection { get; set; }
    }
}
