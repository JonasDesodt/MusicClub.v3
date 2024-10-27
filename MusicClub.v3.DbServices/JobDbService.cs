using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.DbServices
{
    public class JobDbService : IJobService
    {
        public Task<ServiceResult<JobDataResponse>> Create(JobDataRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<JobDataResponse>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<JobDataResponse>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedServiceResult<IList<JobDataResponse>, JobFilterResponse>> GetAll(PaginationRequest paginationRequest, JobFilterRequest filterRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<JobDataResponse>> Update(int id, JobDataRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
