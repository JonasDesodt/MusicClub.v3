using Microsoft.EntityFrameworkCore;
using MusicClub.v3.DbServices.Extensions.GoogleCalendar;
using MusicClub.v3.DbServices.Extensions.GoogleEvent;
using MusicClub.v3.Dto.Data.Response;

namespace MusicClub.v3.DbServices.Extensions.GoogleEvent
{
    internal static class GoogleEventExtensions
    {
        public static IQueryable<DbCore.Models.GoogleEvent> IncludeAll(this IQueryable<DbCore.Models.GoogleEvent> query)
        {
            return query.Include(g => g.Act).ThenInclude(a => a != null ? a.Lineup : null).Include(g => g.GoogleCalendar);
        }

        public static IQueryable<GoogleEventDataResponse> ToResponses(this IQueryable<DbCore.Models.GoogleEvent> query)
        {
            return query.Select(a => new GoogleEventDataResponse
            {
                GoogleIdentifier = a.GoogleIdentifier,
                Created = a.Created,
                Id = a.Id,
                Updated = a.Updated,
                GoogleCalendarDataResponse = a.GoogleCalendar!.ToResponse()  //TODO: temp hack (!), deal w/ null reference
            });
        }

        public static GoogleEventDataResponse ToResponse(this DbCore.Models.GoogleEvent googleEvent)
        {
            return new GoogleEventDataResponse
            {
                GoogleIdentifier = googleEvent.GoogleIdentifier,
                Created = googleEvent.Created,
                Id = googleEvent.Id,
                Updated = googleEvent.Updated,
                GoogleCalendarDataResponse = googleEvent.GoogleCalendar != null ? googleEvent.GoogleCalendar.ToResponse() : null! //TODO: temp hack (!), deal w/ null reference
            };
        }
    }
}
