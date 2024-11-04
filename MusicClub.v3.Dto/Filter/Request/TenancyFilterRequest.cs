using MusicClub.v3.Dto.SourceGeneratorAttributes;
using MusicClub.v3.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicClub.v3.Dto.Filter.Request
{
    [GenerateFilterRequest(typeof(ITenancy))]
    [GenerateFilterResponse(typeof(ITenancy))]
    [GenerateFilterMappers(typeof(ITenancy))]
    [GenerateFilterRequestExtensions(typeof(ITenancy))]
    public partial class TenancyFilterRequest    {    }
}
