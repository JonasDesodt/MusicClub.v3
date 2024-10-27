using MusicClub.v3.Dto.Enums;
using MusicClub.v3.Dto.SourceGeneratorAttributes;
using MusicClub.v3.IModels;
using System.ComponentModel;

namespace MusicClub.v3.Dto.Filter.Request
{
    [GenerateFilterRequest(typeof(IJob))]
    [GenerateFilterResponse(typeof(IJob))]
    [GenerateFilterMappers(typeof(IJob))]
    [GenerateFilterRequestExtensions(typeof(IJob))]
    public partial class JobFilterRequest 
    {
        public string? SortProperty { get; set; }

        //[DefaultValue(SortDirection.Ascending)]
        public SortDirection? SortDirection { get; set; }
    }
}
