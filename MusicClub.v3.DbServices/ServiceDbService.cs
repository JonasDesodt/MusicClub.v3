using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.DbServices
{
    public class ServiceDbService : IServiceService
    {
        public Task<ServiceResult<ServiceDataResponse>> Create(ServiceDataRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<ServiceDataResponse>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<ServiceDataResponse>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedServiceResult<IList<ServiceDataResponse>, ServiceFilterResponse>> GetAll(PaginationRequest paginationRequest, ServiceFilterRequest filterRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<ServiceDataResponse>> Update(int id, ServiceDataRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
