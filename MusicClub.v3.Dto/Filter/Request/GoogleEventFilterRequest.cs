using MusicClub.v3.Dto.Enums;
using MusicClub.v3.Dto.SourceGeneratorAttributes;
using MusicClub.v3.IModels;
using System.ComponentModel;

namespace MusicClub.v3.Dto.Filter.Request
{
    [GenerateFilterRequest(typeof(IGoogleEvent))]
    [GenerateFilterResponse(typeof(IGoogleEvent))]
    [GenerateFilterMappers(typeof(IGoogleEvent))]
    [GenerateFilterRequestExtensions(typeof(IGoogleEvent))]
    public partial class GoogleEventFilterRequest
    {
        public string? SortProperty { get; set; }

        //[DefaultValue(SortDirection.Ascending)]
        public SortDirection? SortDirection { get; set; }
    }
}
