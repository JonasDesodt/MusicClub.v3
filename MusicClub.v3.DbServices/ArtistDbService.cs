using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.DbServices
{
    public class ArtistDbService : IArtistService
    {
        public Task<ServiceResult<ArtistDataResponse>> Create(ArtistDataRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<ArtistDataResponse>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<ArtistDataResponse>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedServiceResult<IList<ArtistDataResponse>, ArtistFilterResponse>> GetAll(PaginationRequest paginationRequest, ArtistFilterRequest filterRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<ArtistDataResponse>> Update(int id, ArtistDataRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
