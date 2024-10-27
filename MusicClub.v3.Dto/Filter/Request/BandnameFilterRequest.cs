using MusicClub.v3.Dto.Enums;
using MusicClub.v3.Dto.SourceGeneratorAttributes;
using MusicClub.v3.IModels;
using System.ComponentModel;

namespace MusicClub.v3.Dto.Filter.Request
{
    [GenerateFilterRequest(typeof(IBandname))]
    [GenerateFilterResponse(typeof(IBandname))]
    [GenerateFilterMappers(typeof(IBandname))]
    [GenerateFilterRequestExtensions(typeof(IBandname))]
    public partial class BandnameFilterRequest 
    {
        public string? SortProperty { get; set; }

        //[DefaultValue(SortDirection.Ascending)]
        public SortDirection? SortDirection { get; set; }
    }
}
