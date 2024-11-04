using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.DbServices
{
    public class TenancyDbService : ITenancyService
    {
        public Task<ServiceResult<TenancyDataResponse>> Create(TenancyDataRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<TenancyDataResponse>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<TenancyDataResponse>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedServiceResult<IList<TenancyDataResponse>, TenancyFilterResponse>> GetAll(PaginationRequest paginationRequest, TenancyFilterRequest filterRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<TenancyDataResponse>> Update(int id, TenancyDataRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
