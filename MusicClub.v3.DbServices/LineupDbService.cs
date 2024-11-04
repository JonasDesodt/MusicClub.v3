using Microsoft.EntityFrameworkCore;
using MusicClub.v3.DbCore;
using MusicClub.v3.DbCore.Models;
using MusicClub.v3.DbServices.Extensions;
using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;
using MusicClub.v3.DbCore.Mappers.IModel;
using MusicClub.v3.DbServices.Extensions.Act;
using MusicClub.v3.DbServices.Extensions.Service;
using MusicClub.v3.DbServices.Extensions.Lineup;
using MusicClub.v3.Dto.Mappers.Filter.Request;

namespace MusicClub.v3.DbServices
{
    public class LineupDbService(MusicClubDbContext dbContext) : ILineupService
    {
        public async Task<ServiceResult<LineupDataResponse>> Create(LineupDataRequest request)
        {
            if (!await dbContext.Images.ExistsOrIsNull(request.ImageId))
            {
                return ((LineupDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Image), request.ImageId).AddNotCreated(nameof(Lineup)));
            }

            var lineup = request.ToModel(dbContext.CurrentTenantId);

            await dbContext.Lineups.AddAsync(lineup);

            await dbContext.SaveChangesAsync();
            return await Get(lineup.Id);
        }

        public async Task<ServiceResult<LineupDataResponse>> Delete(int id)
        {
            if (await dbContext.Lineups.FindAsync(id) is not { } lineup)
            {
                return ((LineupDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Lineup), id).AddNotDeleted(nameof(Lineup), id));
            }

            if (await dbContext.Acts.HasReferenceToLineup(id))
            {
                return ((LineupDataResponse?)null).Wrap(new ServiceMessages().AddReferenceFound(nameof(Lineup), id, nameof(Act)).AddNotDeleted(nameof(Lineup), id));
            }

            if (await dbContext.Services.HasReferenceToLineup(id))
            {
                return ((LineupDataResponse?)null).Wrap(new ServiceMessages().AddReferenceFound(nameof(Lineup), id, nameof(Service)).AddNotDeleted(nameof(Lineup), id));
            }

            dbContext.Lineups.Remove(lineup);

            await dbContext.SaveChangesAsync();

            return ((LineupDataResponse?)null).Wrap();
        }

        public Task<ServiceResult<bool>> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<LineupDataResponse>> Get(int id)
        {
            return (await dbContext.Lineups
                .IncludeAll()
                .ToResponses()
                .FirstOrDefaultAsync(p => p.Id == id))
                .Wrap(new ServiceMessages().AddNotFound(nameof(Lineup), id));
        }

        public async Task<PagedServiceResult<IList<LineupDataResponse>, LineupFilterResponse>> GetAll(PaginationRequest paginationRequest, LineupFilterRequest filterRequest)
        {
            var totalCount = await dbContext.Lineups
                .IncludeAll()
                .Filter(filterRequest)
                .CountAsync();

            return (await dbContext.Lineups
                .IncludeAll()
                .Filter(filterRequest)
                .GetPage(paginationRequest)
                .ToResponses()
                .ToListAsync())
                .Wrap(paginationRequest, totalCount, filterRequest.ToResponse());
        }

        public Task<ServiceResult<bool>> IsReferenced(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<LineupDataResponse>> Update(int id, LineupDataRequest request)
        {
            if (await dbContext.Lineups.IncludeAll().FirstOrDefaultAsync(p => p.Id == id) is not { } lineup)
            {
                return ((LineupDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Lineup), id).AddNotUpdated(nameof(Lineup), id));
            }

            if (!await dbContext.Images.ExistsOrIsNull(request.ImageId))
            {
                return ((LineupDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Image), request.ImageId).AddNotUpdated(nameof(Lineup), id));
            }

            lineup.Update(request);

            await dbContext.SaveChangesAsync();

            return lineup.ToResponse().Wrap();
        }
    }
}
