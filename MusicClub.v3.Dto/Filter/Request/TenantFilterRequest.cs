using MusicClub.v3.Dto.SourceGeneratorAttributes;
using MusicClub.v3.IModels;

namespace MusicClub.v3.Dto.Filter.Request
{
    [GenerateFilterRequest(typeof(ITenant))]
    [GenerateFilterResponse(typeof(ITenant))]
    [GenerateFilterMappers(typeof(ITenant))]
    [GenerateFilterRequestExtensions(typeof(ITenant))]
    public partial class TenantFilterRequest { }
}
