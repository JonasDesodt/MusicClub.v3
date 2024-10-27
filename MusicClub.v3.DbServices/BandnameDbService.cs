using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.DbServices
{
    public class BandnameDbService : IBandnameService
    {
        public Task<ServiceResult<BandnameDataResponse>> Create(BandnameDataRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<BandnameDataResponse>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<BandnameDataResponse>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedServiceResult<IList<BandnameDataResponse>, BandnameFilterResponse>> GetAll(PaginationRequest paginationRequest, BandnameFilterRequest filterRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<BandnameDataResponse>> Update(int id, BandnameDataRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
