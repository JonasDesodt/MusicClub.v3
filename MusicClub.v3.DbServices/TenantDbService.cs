using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.DbServices
{
    public class TenantDbService : ITenantService
    {
        public Task<ServiceResult<TenantDataResponse>> Create(TenantDataRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<TenantDataResponse>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<TenantDataResponse>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedServiceResult<IList<TenantDataResponse>, TenantFilterResponse>> GetAll(PaginationRequest paginationRequest, TenantFilterRequest filterRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<TenantDataResponse>> Update(int id, TenantDataRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
