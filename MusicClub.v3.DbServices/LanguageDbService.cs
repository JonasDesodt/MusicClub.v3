using MusicClub.v3.DbCore;
using MusicClub.v3.DbServices.Extensions.DescriptionTranslation;
using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;
using MusicClub.v3.Dto.Mappers.Filter.Request;
using MusicClub.v3.DbServices.Extensions.Language;
using Microsoft.EntityFrameworkCore;
using MusicClub.v3.DbServices.Extensions;

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

        public async Task<PagedServiceResult<IList<LanguageDataResponse>, LanguageFilterResponse>> GetAll(PaginationRequest paginationRequest, LanguageFilterRequest filterRequest)
        {
            var totalCount = await dbContext.Languages
                .IncludeAll()
                .Filter(filterRequest)
                .CountAsync();

            return (await dbContext.Languages
                .IncludeAll()
                .Filter(filterRequest)
                .GetPage(paginationRequest)
                .ToResponses()
                .ToListAsync())
                .Wrap(paginationRequest, totalCount, filterRequest.ToResponse());
        }

        public Task<ServiceResult<LanguageDataResponse>> Update(int id, LanguageDataRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
