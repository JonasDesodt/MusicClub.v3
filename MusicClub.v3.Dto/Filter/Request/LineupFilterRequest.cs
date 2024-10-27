using MusicClub.v3.Dto.Enums;
using MusicClub.v3.Dto.SourceGeneratorAttributes;
using MusicClub.v3.IModels;
using System.ComponentModel;

namespace MusicClub.v3.Dto.Filter.Request
{
    [GenerateFilterRequest(typeof(ILineup))]
    [GenerateFilterResponse(typeof(ILineup))]
    [GenerateFilterMappers(typeof(ILineup))]
    [GenerateFilterRequestExtensions(typeof(ILineup))]
    public partial class LineupFilterRequest 
    {
        public string? SortProperty { get; set; }

        //[DefaultValue(SortDirection.Ascending)]
        public SortDirection? SortDirection { get; set; }
    }
}
