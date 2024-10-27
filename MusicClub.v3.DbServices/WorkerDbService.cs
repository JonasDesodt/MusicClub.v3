using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.DbServices
{
    public class WorkerDbService : IWorkerService
    {
        public Task<ServiceResult<WorkerDataResponse>> Create(WorkerDataRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<WorkerDataResponse>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<WorkerDataResponse>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedServiceResult<IList<WorkerDataResponse>, WorkerFilterResponse>> GetAll(PaginationRequest paginationRequest, WorkerFilterRequest filterRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<WorkerDataResponse>> Update(int id, WorkerDataRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
