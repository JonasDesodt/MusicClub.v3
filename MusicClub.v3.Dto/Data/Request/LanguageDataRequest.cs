using MusicClub.v3.Dto.SourceGeneratorAttributes;
using MusicClub.v3.IModels;

namespace MusicClub.v3.Dto.Data.Request
{
    [GenerateIModelImplementation]
    [GenerateDataResponse]
    [GenerateDataMappers]
    public partial class LanguageDataRequest : ILanguage { }
}
