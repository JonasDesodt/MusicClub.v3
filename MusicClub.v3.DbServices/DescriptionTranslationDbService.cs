using Microsoft.EntityFrameworkCore;
using MusicClub.v3.DbCore;
using MusicClub.v3.DbServices.Extensions;
using MusicClub.v3.DbServices.Extensions.Artist;
using MusicClub.v3.DbServices.Extensions.DescriptionTranslation;
using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Mappers.Filter.Request;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.DbServices
{
    public class DescriptionTranslationDbService(MusicClubDbContext dbContext) : IDescriptionTranslationService
    {
        public Task<ServiceResult<DescriptionTranslationDataResponse>> Create(DescriptionTranslationDataRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<DescriptionTranslationDataResponse>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<DescriptionTranslationDataResponse>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedServiceResult<IList<DescriptionTranslationDataResponse>, DescriptionTranslationFilterResponse>> GetAll(PaginationRequest paginationRequest, DescriptionTranslationFilterRequest filterRequest)
        {
            var totalCount = await dbContext.DescriptionTranslations
                .IncludeAll()
                .Filter(filterRequest)
                .CountAsync();

            return (await dbContext.DescriptionTranslations
                .IncludeAll()
                .Filter(filterRequest)
                .GetPage(paginationRequest)
                .ToResponses()
                .ToListAsync())
                .Wrap(paginationRequest, totalCount, filterRequest.ToResponse());
        }

        public Task<ServiceResult<DescriptionTranslationDataResponse>> Update(int id, DescriptionTranslationDataRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
