using MusicClub.v3.DbCore.Models;
using MusicClub.v3.DbCore;
using MusicClub.v3.DbServices.Extensions;
using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Mappers.Filter.Request;
using MusicClub.v3.Dto.Transfer;
using MusicClub.v3.DbServices.Extensions.Performance;
using MusicClub.v3.DbServices.Extensions.Job;
using MusicClub.v3.DbServices.Extensions.Act;
using Microsoft.EntityFrameworkCore;
using MusicClub.v3.DbServices.Extensions.Image;
using MusicClub.v3.DbServices.Extensions.Lineup;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.DbCore.Mappers.IModel;

namespace MusicClub.v3.DbServices
{
    public class ActDbService(MusicClubDbContext dbContext) : IActService
    {
        public async Task<ServiceResult<ActDataResponse>> Create(ActDataRequest dataRequest)
        {
            if (!await dbContext.Lineups.ExistsOrIsNull(dataRequest.LineupId))
            {
                return ((ActDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Lineup), dataRequest.LineupId).AddNotCreated(nameof(Act)));
            }

            if (!await dbContext.Images.ExistsOrIsNull(dataRequest.ImageId))
            {
                return ((ActDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Image), dataRequest.ImageId).AddNotCreated(nameof(Act)));
            }

            var act = dataRequest.ToModel(dbContext.CurrentTenantId);

            await dbContext.Acts.AddAsync(act);

            await dbContext.SaveChangesAsync();

            return await Get(act.Id);
        }

        public async Task<ServiceResult<ActDataResponse>> Delete(int id)
        {
            if (await dbContext.Acts.FindAsync(id) is not { } act)
            {
                return ((ActDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Act), id).AddNotDeleted(nameof(Act), id));
            }

            if (await dbContext.Performances.HasReferenceToAct(id))
            {
                return ((ActDataResponse?)null).Wrap(new ServiceMessages().AddReferenceFound(nameof(Act), id, nameof(Performance)).AddNotDeleted(nameof(Act), id));
            }

            if (await dbContext.Jobs.HasReferenceToAct(id))
            {
                return ((ActDataResponse?)null).Wrap(new ServiceMessages().AddReferenceFound(nameof(Act), id, nameof(Job)).AddNotDeleted(nameof(Act), id));
            }

            dbContext.Acts.Remove(act);

            await dbContext.SaveChangesAsync();

            return ((ActDataResponse?)null).Wrap();
        }

        public Task<ServiceResult<bool>> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<ActDataResponse>> Get(int id)
        {
            return (await dbContext.Acts
                .IncludeAll()
                .ToResponses()
                .FirstOrDefaultAsync(p => p.Id == id))
                .Wrap(new ServiceMessages().AddNotFound(nameof(Act), id));
        }

        public async Task<PagedServiceResult<IList<ActDataResponse>, ActFilterResponse>> GetAll(PaginationRequest paginationRequest, ActFilterRequest filterRequest)
        {
            var filterResult = filterRequest.ToResponse();

            if (filterRequest.ImageId > 0)
            {
                if (await dbContext.Images
                .IncludeAll()
                .ToResponses()
                .FirstOrDefaultAsync(i => i.Id == filterRequest.ImageId) is not { } imageDataResponse)
                {
                    return ((IList<ActDataResponse>?)null).Wrap(paginationRequest, 0, filterRequest.ToResponse(), new ServiceMessages().AddNotFound(nameof(Image), filterRequest.ImageId));
                }
                else
                {
                    filterResult.ImageDataResponse = imageDataResponse;
                }
            }

            if (filterRequest.LineupId > 0)
            {
                if (await dbContext.Lineups
                .IncludeAll()
                .ToResponses()
                .FirstOrDefaultAsync(i => i.Id == filterRequest.LineupId) is not { } lineupDataResponse)
                {
                    return ((IList<ActDataResponse>?)null).Wrap(paginationRequest, 0, filterRequest.ToResponse(), new ServiceMessages().AddNotFound(nameof(Lineup), filterRequest.LineupId));
                }
                else
                {
                    filterResult.LineupDataResponse = lineupDataResponse;
                }
            }

            var totalCount = await dbContext.Acts
                .IncludeAll()
                .Filter(filterRequest)
                .CountAsync();

            return (await dbContext.Acts
                .IncludeAll()
                .Filter(filterRequest)
                .GetPage(paginationRequest)
                .ToResponses()
                .ToListAsync())
                .Wrap(paginationRequest, totalCount, filterResult);
        }

        public Task<ServiceResult<bool>> IsReferenced(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<ActDataResponse>> Update(int id, ActDataRequest request)
        {
            if (await dbContext.Acts.IncludeAll().FirstOrDefaultAsync(p => p.Id == id) is not { } act)
            {
                return ((ActDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Act), id).AddNotUpdated(nameof(Act), id));
            }

            if (!await dbContext.Lineups.ExistsOrIsNull(request.LineupId))
            {
                return ((ActDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Lineup), request.LineupId).AddNotUpdated(nameof(Act), id));
            }

            if (!await dbContext.Images.ExistsOrIsNull(request.ImageId))
            {
                return ((ActDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Image), request.ImageId).AddNotUpdated(nameof(Act), id));
            }

            act.Update(request);

            await dbContext.SaveChangesAsync();

            return act.ToResponse().Wrap();
        }
    }
}
