using MusicClub.v3.Dto.Enums;
using MusicClub.v3.Dto.SourceGeneratorAttributes;
using MusicClub.v3.IModels;

namespace MusicClub.v3.Dto.Filter.Request
{
    [GenerateFilterRequest(typeof(ILanguage))]
    [GenerateFilterResponse(typeof(ILanguage))]
    [GenerateFilterMappers(typeof(ILanguage))]
    [GenerateFilterRequestExtensions(typeof(ILanguage))]
    public partial class LanguageFilterRequest 
    {
        public string? SortProperty { get; set; }

        //[DefaultValue(SortDirection.Ascending)]
        public SortDirection? SortDirection { get; set; }
    }
}
