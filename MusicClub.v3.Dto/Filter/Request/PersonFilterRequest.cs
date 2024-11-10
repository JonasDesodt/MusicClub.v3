using MusicClub.v3.Dto.Enums;
using MusicClub.v3.Dto.SourceGeneratorAttributes;
using MusicClub.v3.IModels;
using System.ComponentModel;

namespace MusicClub.v3.Dto.Filter.Request
{
    [GenerateFilterRequest(typeof(IPerson))]
    [GenerateFilterResponse(typeof(IPerson))]
    [GenerateFilterMappers(typeof(IPerson))]
    [GenerateFilterRequestExtensions(typeof(IPerson))]
    public partial class PersonFilterRequest 
    {
        public string? EmailAddress { get; set; }

        public string? SortProperty { get; set; }

        //[DefaultValue(SortDirection.Ascending)]
        public SortDirection? SortDirection { get; set; }
    }
}
