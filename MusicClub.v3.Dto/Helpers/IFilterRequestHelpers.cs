using MusicClub.v3.Dto.SourceGeneratorAttributes;

namespace MusicClub.v3.Dto.Helpers
{
    [GenerateFilterRequestHelpers( // todo => use the IModels ?
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
    public interface IFilterRequestHelpers<TFilterRequest, TFilterResponse>
    {
        string ToQueryString(TFilterRequest filterRequest);

        //TFilterResponse ToResponse(TFilterRequest filterRequest);
    }
}
