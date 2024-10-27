using MusicClub.v3.Dto.Enums;
using MusicClub.v3.Dto.SourceGeneratorAttributes;
using MusicClub.v3.IModels;
using System.ComponentModel;

namespace MusicClub.v3.Dto.Filter.Request
{
    [GenerateFilterRequest(typeof(IPerformance))]
    [GenerateFilterResponse(typeof(IPerformance))]
    [GenerateFilterMappers(typeof(IPerformance))]
    [GenerateFilterRequestExtensions(typeof(IPerformance))]
    public partial class PerformanceFilterRequest
    {
        public string? SortProperty { get; set; }

        //[DefaultValue(SortDirection.Ascending)]
        public SortDirection? SortDirection { get; set; }
    }
}
