using MusicClub.v3.Dto.Enums;
using MusicClub.v3.Dto.SourceGeneratorAttributes;
using MusicClub.v3.IModels;
using System.ComponentModel;

namespace MusicClub.v3.Dto.Filter.Request
{
    [GenerateFilterRequest(typeof(IGoogleCalendar))]
    [GenerateFilterResponse(typeof(IGoogleCalendar))]
    [GenerateFilterMappers(typeof(IGoogleCalendar))]
    [GenerateFilterRequestExtensions(typeof(IGoogleCalendar))]
    public partial class GoogleCalendarFilterRequest 
    {
        public string? SortProperty { get; set; }

        //[DefaultValue(SortDirection.Ascending)]
        public SortDirection? SortDirection { get; set; }
    }
}
