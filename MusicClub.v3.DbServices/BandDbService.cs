using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.DbServices
{
    public class BandDbService : IBandService
    {
        public Task<ServiceResult<BandDataResponse>> Create(BandDataRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<BandDataResponse>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<BandDataResponse>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedServiceResult<IList<BandDataResponse>, BandFilterResponse>> GetAll(PaginationRequest paginationRequest, BandFilterRequest filterRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<BandDataResponse>> Update(int id, BandDataRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
