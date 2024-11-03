using MusicClub.v3.Dto.Enums;
using MusicClub.v3.Dto.SourceGeneratorAttributes;
using MusicClub.v3.IModels;

namespace MusicClub.v3.Dto.Filter.Request
{
    [GenerateFilterRequest(typeof(IDescriptionTranslation))]
    [GenerateFilterResponse(typeof(IDescriptionTranslation))]
    [GenerateFilterMappers(typeof(IDescriptionTranslation))]
    [GenerateFilterRequestExtensions(typeof(IDescriptionTranslation))]
    public partial class DescriptionTranslationFilterRequest
    {
        public string? SortProperty { get; set; }

        //[DefaultValue(SortDirection.Ascending)]
        public SortDirection? SortDirection { get; set; }
    }
}
