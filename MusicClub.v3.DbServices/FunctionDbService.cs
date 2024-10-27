using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.DbServices
{
    public class FunctionDbService : IFunctionService
    {
        public Task<ServiceResult<FunctionDataResponse>> Create(FunctionDataRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<FunctionDataResponse>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<FunctionDataResponse>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedServiceResult<IList<FunctionDataResponse>, FunctionFilterResponse>> GetAll(PaginationRequest paginationRequest, FunctionFilterRequest filterRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<FunctionDataResponse>> Update(int id, FunctionDataRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
