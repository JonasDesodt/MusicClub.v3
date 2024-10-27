using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.DbServices
{
    public class GoogleCalendarDbService : IGoogleCalendarService
    {
        public Task<ServiceResult<GoogleCalendarDataResponse>> Create(GoogleCalendarDataRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<GoogleCalendarDataResponse>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<GoogleCalendarDataResponse>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedServiceResult<IList<GoogleCalendarDataResponse>, GoogleCalendarFilterResponse>> GetAll(PaginationRequest paginationRequest, GoogleCalendarFilterRequest filterRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<GoogleCalendarDataResponse>> Update(int id, GoogleCalendarDataRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
