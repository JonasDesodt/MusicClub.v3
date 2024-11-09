using MusicClub.v3.Dto.SourceGeneratorAttributes;

namespace MusicClub.v3.Dto.Helpers
{
    [GenerateFilterResponseHelpers( // todo => use the IModels ?
        "Act",
        "Artist",
        "Band",
        "Bandname",
        "Description",
        "DescriptionTranslation",
        "Function",
        "Job",
        "Language",
        "Lineup",
        "Performance",
        "Person",
        "Service",
        "Worker")]
    public interface IFilterResponseHelpers<TFilterRequest, TFilterResponse>
    {
        [ClassSuffix("FilterResponseMappers")]
        TFilterRequest ToRequest(TFilterResponse filterResponse);
    }
}
