using MusicClub.v3.Dto.Enums;
using MusicClub.v3.Dto.SourceGeneratorAttributes;
using MusicClub.v3.IModels;
using System.ComponentModel;

namespace MusicClub.v3.Dto.Filter.Request
{
    [GenerateFilterRequest(typeof(IWorker))]
    [GenerateFilterResponse(typeof(IWorker))]
    [GenerateFilterMappers(typeof(IWorker))]
    [GenerateFilterRequestExtensions(typeof(IWorker))]
    public partial class WorkerFilterRequest 
    {
        public string? SortProperty { get; set; }

        //[DefaultValue(SortDirection.Ascending)]
        public SortDirection? SortDirection { get; set; }
    }
}
