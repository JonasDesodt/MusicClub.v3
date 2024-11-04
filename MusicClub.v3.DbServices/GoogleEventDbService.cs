using MusicClub.v3.DbCore.Mappers.IModel;
using MusicClub.v3.DbCore.Models;
using MusicClub.v3.DbCore;
using MusicClub.v3.DbServices.Extensions;
using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;
using MusicClub.v3.DbServices.Extensions.GoogleEvent;
using Microsoft.EntityFrameworkCore;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Calendar.v3;

namespace MusicClub.v3.DbServices
{
    public class GoogleEventDbService(MusicClubDbContext dbContext, CalendarService googleCalendarService) : IGoogleEventService
    {
        private const GoogleEventDataResponse? EmptyDataResponse = null;

        public async Task<ServiceResult<GoogleEventDataResponse>> Create(GoogleEventDataRequest request)
        {
            var act = await dbContext.Acts.FindAsync(request.ActId);
            if (act is null)
            {
                return EmptyDataResponse.Wrap();
            }

            if (act.GoogleEventId > 0)
            {
                return EmptyDataResponse.Wrap();
            }

            if (act.Start is null)
            {
                return EmptyDataResponse.Wrap();
            }

            if (!(act.Duration > 0))
            {
                return EmptyDataResponse.Wrap();
            }


            var googleCalendar = await dbContext.GoogleCalendars.FindAsync(request.GoogleCalendarId);
            if (googleCalendar is null)
            {
                return EmptyDataResponse.Wrap();
            }


            var eventRequest = new Event
            {
                Summary = act.Name,
                Description = act.DescriptionId.ToString(), // todo => temp hack, include the Description!
                Start = new EventDateTime
                {
                    DateTimeRaw = act.Start?.ToString("o"),
                    TimeZone = "Europe/Brussels"
                },
                End = new EventDateTime
                {
                    DateTimeRaw = (act.Start?.AddMinutes((double)act.Duration))?.ToString("o"),
                    TimeZone = "Europe/Brussels"
                }
            };

            var eventResponse = await googleCalendarService.Events.Insert(eventRequest, googleCalendar.GoogleIdentifier).ExecuteAsync();
            if (eventResponse is null)
            {
                return EmptyDataResponse.Wrap();
            }

            var now = DateTime.UtcNow;
            var googleEvent = new GoogleEvent
            {
                Created = now,
                Updated = now,
                GoogleCalendarId = googleCalendar.Id,
                GoogleIdentifier = eventResponse.Id,
                TenantId = dbContext.CurrentTenantId
            };

            await dbContext.GoogleEvents.AddAsync(googleEvent);
            await dbContext.SaveChangesAsync();

            act.GoogleEventId = googleEvent.Id;
            dbContext.Acts.Update(act);
            await dbContext.SaveChangesAsync();

            return await Get(googleEvent.Id);
        }

        public Task<ServiceResult<GoogleEventDataResponse>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<bool>> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<GoogleEventDataResponse>> Get(int id)
        {
            return (await dbContext.GoogleEvents
                .IncludeAll()
                .ToResponses()
                .FirstOrDefaultAsync(p => p.Id == id))
                .Wrap(new ServiceMessages().AddNotFound(nameof(GoogleEvent), id));
        }

        public Task<PagedServiceResult<IList<GoogleEventDataResponse>, GoogleEventFilterResponse>> GetAll(PaginationRequest paginationRequest, GoogleEventFilterRequest filterRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<bool>> IsReferenced(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<GoogleEventDataResponse>> Update(int id, GoogleEventDataRequest request)
        {
            var googleEvent = await dbContext.GoogleEvents.FindAsync(id);
            if (googleEvent == null)
            {
                return EmptyDataResponse.Wrap();
            }


            var act = await dbContext.Acts.FindAsync(request.ActId);
            if (act is null)
            {
                return EmptyDataResponse.Wrap();
            }

            if (act.GoogleEventId is null)
            {
                return EmptyDataResponse.Wrap();
            }

            if (act.Start is null)
            {
                return EmptyDataResponse.Wrap();
            }

            if (!(act.Duration > 0))
            {
                return EmptyDataResponse.Wrap();
            }



            var googleCalendar = await dbContext.GoogleCalendars.FindAsync(request.GoogleCalendarId);
            if (googleCalendar is null)
            {
                return EmptyDataResponse.Wrap();
            }



            var currentEventResponse = await googleCalendarService.Events.Get(googleCalendar.GoogleIdentifier, googleEvent.GoogleIdentifier).ExecuteAsync();
            if (currentEventResponse is null)
            {
                return EmptyDataResponse.Wrap();
            }

            var eventRequest = new Event
            {
                Summary = act.Name,
                Description = act.Title,
                Start = new EventDateTime
                {
                    DateTimeRaw = act.Start?.ToString("o"),
                    TimeZone = "Europe/Brussels"
                },
                End = new EventDateTime
                {
                    DateTimeRaw = (act.Start?.AddMinutes((double)act.Duration))?.ToString("o"),
                    TimeZone = "Europe/Brussels"
                }
            };

            var updatedEventResponse = await googleCalendarService.Events.Update(eventRequest, googleCalendar.GoogleIdentifier, googleEvent.GoogleIdentifier).ExecuteAsync();
            if (updatedEventResponse is null)
            {
                return EmptyDataResponse.Wrap();
            }


            return await Get(googleEvent.Id);
        }
    }
}
