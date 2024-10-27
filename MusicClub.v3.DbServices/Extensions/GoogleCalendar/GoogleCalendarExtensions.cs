using Microsoft.EntityFrameworkCore;
using MusicClub.v3.Dto.Data.Response;

namespace MusicClub.v3.DbServices.Extensions.GoogleCalendar
{
    internal static class GoogleCalendarExtensions
    {
        public static IQueryable<DbCore.Models.GoogleCalendar> IncludeAll(this IQueryable<DbCore.Models.GoogleCalendar> query)
        {
            return query.Include(g => g.GoogleEvents);
        }

        public static IQueryable<GoogleCalendarDataResponse> ToResponses(this IQueryable<DbCore.Models.GoogleCalendar> query)
        {
            return query.Select(a => new GoogleCalendarDataResponse
            {
                GoogleEventsCount = a.GoogleEvents.Count,
                GoogleIdentifier = a.GoogleIdentifier,
                Created = a.Created,
                Id = a.Id,
                Updated = a.Updated,
            });
        }

        public static GoogleCalendarDataResponse ToResponse(this DbCore.Models.GoogleCalendar googleCalendar)
        {
            return new GoogleCalendarDataResponse
            {
                GoogleEventsCount = googleCalendar.GoogleEvents.Count,
                GoogleIdentifier = googleCalendar.GoogleIdentifier,
                Created = googleCalendar.Created,
                Id = googleCalendar.Id,
                Updated = googleCalendar.Updated,
            };
        }
    }
}
