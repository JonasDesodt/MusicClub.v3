using MusicClub.v3.DbCore;
using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.DbServices
{
    public class LanguageDbService(MusicClubDbContext dbContext) : ILanguageService
    {
        public Task<ServiceResult<LanguageDataResponse>> Create(LanguageDataRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<LanguageDataResponse>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<LanguageDataResponse>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedServiceResult<IList<LanguageDataResponse>, LanguageFilterResponse>> GetAll(PaginationRequest paginationRequest, LanguageFilterRequest filterRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<LanguageDataResponse>> Update(int id, LanguageDataRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
