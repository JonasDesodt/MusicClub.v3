using MusicClub.v3.Dto.Enums;
using MusicClub.v3.Dto.Filter.Interfaces;
using MusicClub.v3.Dto.SourceGeneratorAttributes;
using System.ComponentModel;

namespace MusicClub.v3.Dto.Filter.Request
{
    [GenerateFilterRequest(typeof(IImageFilterRequest))]
    [GenerateFilterResponse(typeof(IImageFilterRequest))]
    [GenerateFilterMappers(typeof(IImageFilterRequest))]
    [GenerateFilterRequestExtensions(typeof(IImageFilterRequest))]
    public partial class ImageFilterRequest
    {
        public string? SortProperty { get; set; }

        //[DefaultValue(SortDirection.Ascending)]
        public SortDirection? SortDirection { get; set; }
    }
}
