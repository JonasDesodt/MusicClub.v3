using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.DbServices
{
    public class PerformanceDbService : IPerformanceService
    {
        public Task<ServiceResult<PerformanceDataResponse>> Create(PerformanceDataRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<PerformanceDataResponse>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<PerformanceDataResponse>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedServiceResult<IList<PerformanceDataResponse>, PerformanceFilterResponse>> GetAll(PaginationRequest paginationRequest, PerformanceFilterRequest filterRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<PerformanceDataResponse>> Update(int id, PerformanceDataRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
