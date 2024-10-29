using Microsoft.EntityFrameworkCore;
using MusicClub.v3.DbServices.Extensions.Act;
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
            return query.Select(g => new GoogleEventDataResponse
            {
                Created = g.Created,
                Id = g.Id,
                Updated = g.Updated,
                GoogleCalendarDataResponse = g.GoogleCalendar != null ? g.GoogleCalendar.ToResponse() : null!,  //TODO: temp hack (!), deal w/ null reference
                ActDataResponse = g.Act != null ? g.Act.ToResponse() : null!
            });
        }

        public static GoogleEventDataResponse ToResponse(this DbCore.Models.GoogleEvent googleEvent)
        {
            return new GoogleEventDataResponse
            {
                Created = googleEvent.Created,
                Id = googleEvent.Id,
                Updated = googleEvent.Updated,
                GoogleCalendarDataResponse = googleEvent.GoogleCalendar != null ? googleEvent.GoogleCalendar.ToResponse() : null!, //TODO: temp hack (!), deal w/ null reference
                ActDataResponse = googleEvent.Act != null ? googleEvent.Act.ToResponse() : null!
            };
        }
    }
}
