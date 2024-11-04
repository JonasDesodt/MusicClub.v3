using MusicClub.v3.Dto.SourceGeneratorAttributes;
using MusicClub.v3.IModels;

namespace MusicClub.v3.Dto.Filter.Request
{
    [GenerateFilterRequest(typeof(IApiKey))]
    [GenerateFilterResponse(typeof(IApiKey))]
    [GenerateFilterMappers(typeof(IApiKey))]
    [GenerateFilterRequestExtensions(typeof(IApiKey))]
    public partial class ApiKeyFilterRequest { }
}
