using MusicClub.v3.DbCore;
using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.DbServices
{
    public class DescriptionDbService(MusicClubDbContext dbContext) : IDescriptionService
    {
        public Task<ServiceResult<DescriptionDataResponse>> Create(DescriptionDataRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<DescriptionDataResponse>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<DescriptionDataResponse>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedServiceResult<IList<DescriptionDataResponse>, DescriptionFilterResponse>> GetAll(PaginationRequest paginationRequest, DescriptionFilterRequest filterRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<DescriptionDataResponse>> Update(int id, DescriptionDataRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
