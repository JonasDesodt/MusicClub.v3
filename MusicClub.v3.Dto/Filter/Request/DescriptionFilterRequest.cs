using MusicClub.v3.Dto.Enums;
using MusicClub.v3.Dto.SourceGeneratorAttributes;
using MusicClub.v3.IModels;

namespace MusicClub.v3.Dto.Filter.Request
{
    [GenerateFilterRequest(typeof(IDescription))]
    [GenerateFilterResponse(typeof(IDescription))]
    [GenerateFilterMappers(typeof(IDescription))]
    [GenerateFilterRequestExtensions(typeof(IDescription))]
    public partial class DescriptionFilterRequest
    {
        public string? SortProperty { get; set; }

        //[DefaultValue(SortDirection.Ascending)]
        public SortDirection? SortDirection { get; set; }
    }
}
