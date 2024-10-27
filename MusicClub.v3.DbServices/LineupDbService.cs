
using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.DbServices
{
    public class LineupDbService : ILineupService
    {
        public Task<ServiceResult<LineupDataResponse>> Create(LineupDataRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<LineupDataResponse>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<LineupDataResponse>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedServiceResult<IList<LineupDataResponse>, LineupFilterResponse>> GetAll(PaginationRequest paginationRequest, LineupFilterRequest filterRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<LineupDataResponse>> Update(int id, LineupDataRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
