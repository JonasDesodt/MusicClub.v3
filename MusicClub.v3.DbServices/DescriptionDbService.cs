using MusicClub.v3.DbCore;
using MusicClub.v3.DbCore.Models;
using MusicClub.v3.DbServices.Extensions.Artist;
using MusicClub.v3.DbServices.Extensions;
using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;
using Microsoft.EntityFrameworkCore;
using MusicClub.v3.DbServices.Extensions.Description;

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

        public async Task<ServiceResult<DescriptionDataResponse>> Get(int id)
        {
            return (await dbContext.Descriptions
                .IncludeAll()
                .ToResponses()
                .FirstOrDefaultAsync(p => p.Id == id))
                .Wrap(new ServiceMessages().AddNotFound(nameof(Description), id));
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
