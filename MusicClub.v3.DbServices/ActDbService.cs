using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.DbServices
{
    public class ActDbService : IActService
    {
        public Task<ServiceResult<ActDataResponse>> Create(ActDataRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<ActDataResponse>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<ActDataResponse>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedServiceResult<IList<ActDataResponse>, ActFilterResponse>> GetAll(PaginationRequest paginationRequest, ActFilterRequest filterRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<ActDataResponse>> Update(int id, ActDataRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
