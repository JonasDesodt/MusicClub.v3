using MusicClub.v3.DbCore.Models;
using MusicClub.v3.DbCore;
using MusicClub.v3.DbServices.Extensions;
using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;
using MusicClub.v3.DbServices.Extensions.GoogleCalendar;
using Microsoft.EntityFrameworkCore;
using MusicClub.v3.DbCore.Mappers.IModel;

namespace MusicClub.v3.DbServices
{
    public class GoogleCalendarDbService(MusicClubDbContext dbContext) : IGoogleCalendarService
    {
        private const GoogleCalendarDataResponse? EmptyDataResult = null;

        public async Task<ServiceResult<GoogleCalendarDataResponse>> Create(GoogleCalendarDataRequest request)
        {
            if (dbContext.GoogleCalendars.Any(g => g.GoogleIdentifier.ToLower() == request.GoogleIdentifier.ToLower()))
            {
                return EmptyDataResult.Wrap(new ServiceMessages().AddDuplicate(nameof(GoogleCalendar)).AddNotCreated(nameof(GoogleCalendar)));
            }

            var googleCalendar = request.ToModel();

            await dbContext.GoogleCalendars.AddAsync(googleCalendar);

            await dbContext.SaveChangesAsync();

            return await Get(googleCalendar.Id);
        }

        public Task<ServiceResult<GoogleCalendarDataResponse>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<bool>> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<GoogleCalendarDataResponse>> Get(int id)
        {
            return (await dbContext.GoogleCalendars
                .IncludeAll()
                .ToResponses()
                .FirstOrDefaultAsync(p => p.Id == id))
                .Wrap(new ServiceMessages().AddNotFound(nameof(GoogleCalendar), id));
        }

        public Task<PagedServiceResult<IList<GoogleCalendarDataResponse>, GoogleCalendarFilterResponse>> GetAll(PaginationRequest paginationRequest, GoogleCalendarFilterRequest filterRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<bool>> IsReferenced(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<GoogleCalendarDataResponse>> Update(int id, GoogleCalendarDataRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
