using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.DbServices
{
    public class GoogleEventDbService : IGoogleEventService
    {
        public Task<ServiceResult<GoogleEventDataResponse>> Create(GoogleEventDataRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<GoogleEventDataResponse>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<GoogleEventDataResponse>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedServiceResult<IList<GoogleEventDataResponse>, GoogleEventFilterResponse>> GetAll(PaginationRequest paginationRequest, GoogleEventFilterRequest filterRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<GoogleEventDataResponse>> Update(int id, GoogleEventDataRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
