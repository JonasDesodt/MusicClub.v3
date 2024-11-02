using MusicClub.v3.Dto.Enums;
using MusicClub.v3.Dto.SourceGeneratorAttributes;
using MusicClub.v3.Dto.Transfer;
using MusicClub.v3.IModels;
namespace MusicClub.v3.Dto.Filter.Request
{
    [GenerateFilterRequest(typeof(ILineup))]
    [GenerateFilterResponse(typeof(ILineup))]
    [GenerateFilterMappers(typeof(ILineup))]
    [GenerateFilterRequestExtensions(typeof(ILineup))]
    public partial class LineupFilterRequest 
    {
        public string? DeepSearch { get; set; }

        public Between<DateTime>? Between { get; set; }

        public string? SortProperty { get; set; }

        //[DefaultValue(SortDirection.Ascending)]
        public SortDirection? SortDirection { get; set; }
    }
}
