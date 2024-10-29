using MusicClub.v3.Dto.SourceGeneratorAttributes;
using MusicClub.v3.IModels;

namespace MusicClub.v3.Dto.Data.Request
{
    //[GenerateIModelImplementation]
    [GenerateDataResponse] // todo => the foreign keys don't turn into dataresponses in the  GoogleEventDataResponse
    [GenerateDataMappers]
    public /*partial*/ class GoogleEventDataRequest// : IGoogleEvent 
    {
        public required int GoogleCalendarId { get; set; }
        public required int ActId { get; set; }
    }
}
