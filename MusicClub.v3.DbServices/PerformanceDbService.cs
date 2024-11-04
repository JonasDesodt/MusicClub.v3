using Microsoft.EntityFrameworkCore;
using MusicClub.v3.DbCore;
using MusicClub.v3.DbCore.Models;
using MusicClub.v3.DbCore.Mappers.IModel;
using MusicClub.v3.DbServices.Extensions;
using MusicClub.v3.DbServices.Extensions.Act;
using MusicClub.v3.DbServices.Extensions.Artist;
using MusicClub.v3.DbServices.Extensions.Bandname;
using MusicClub.v3.DbServices.Extensions.Image;
using MusicClub.v3.DbServices.Extensions.Performance;
using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;
using MusicClub.v3.Dto.Extensions;
using MusicClub.v3.Dto.Mappers.Filter.Request;

namespace MusicClub.v3.DbServices
{
    public class PerformanceDbService(MusicClubDbContext dbContext) : IPerformanceService
    {
        public async Task<ServiceResult<PerformanceDataResponse>> Create(PerformanceDataRequest request)
        {
            if (!await dbContext.Acts.ExistsOrIsNull(request.ActId))
            {
                return ((PerformanceDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Act), request.ActId).AddNotCreated(nameof(Performance)));
            }

            if (!await dbContext.Artists.ExistsOrIsNull(request.ArtistId))
            {
                return ((PerformanceDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Artist), request.ArtistId).AddNotCreated(nameof(Performance)));
            }

            if (!await dbContext.Bandnames.ExistsOrIsNull(request.BandnameId))
            {
                return ((PerformanceDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Bandname), request.BandnameId).AddNotCreated(nameof(Performance)));
            }

            if (!await dbContext.Images.ExistsOrIsNull(request.ImageId))
            {
                return ((PerformanceDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Image), request.ImageId).AddNotCreated(nameof(Performance)));
            }

            var performance = request.ToModel(dbContext.CurrentTenantId);

            await dbContext.Performances.AddAsync(performance);

            await dbContext.SaveChangesAsync();

            return await Get(performance.Id);
        }

        public async Task<ServiceResult<PerformanceDataResponse>> Delete(int id)
        {
            if (await dbContext.Performances.FindAsync(id) is not { } performance)
            {
                return ((PerformanceDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Performance), id).AddNotDeleted(nameof(Performance), id));
            }

            dbContext.Performances.Remove(performance);

            await dbContext.SaveChangesAsync();

            return ((PerformanceDataResponse?)null).Wrap();
        }

        public async Task<ServiceResult<PerformanceDataResponse>> Get(int id)
        {
            return (await dbContext.Performances
             .IncludeAll()
             .ToResponses()
             .FirstOrDefaultAsync(p => p.Id == id))
             .Wrap(new ServiceMessages().AddNotFound(nameof(Performance), id));
        }

        public async Task<PagedServiceResult<IList<PerformanceDataResponse>, PerformanceFilterResponse>> GetAll(PaginationRequest paginationRequest, PerformanceFilterRequest filterRequest)
        {
            var filterResult = filterRequest.ToResponse();

            if (filterRequest.ActId > 0)
            {
                if (await dbContext.Acts
                .IncludeAll()
                .ToResponses()
                .FirstOrDefaultAsync(i => i.Id == filterRequest.ActId) is not { } actDataResponse)
                {
                    return ((IList<PerformanceDataResponse>?)null).Wrap(paginationRequest, 0, filterRequest.ToResponse(), new ServiceMessages().AddNotFound(nameof(Act), filterRequest.ActId));
                }
                else
                {
                    filterResult.ActDataResponse = actDataResponse;
                }
            }

            if (filterRequest.ArtistId > 0)
            {
                if (await dbContext.Artists
                .IncludeAll()
                .ToResponses()
                .FirstOrDefaultAsync(i => i.Id == filterRequest.ArtistId) is not { } artistDataResponse)
                {
                    return ((IList<PerformanceDataResponse>?)null).Wrap(paginationRequest, 0, filterRequest.ToResponse(), new ServiceMessages().AddNotFound(nameof(Artist), filterRequest.ArtistId));
                }
                else
                {
                    filterResult.ArtistDataResponse = artistDataResponse;
                }
            }

            if (filterRequest.BandnameId > 0)
            {
                if (await dbContext.Bandnames
                .IncludeAll()
                .ToResponses()
                .FirstOrDefaultAsync(i => i.Id == filterRequest.BandnameId) is not { } bandnameDataResponse)
                {
                    return ((IList<PerformanceDataResponse>?)null).Wrap(paginationRequest, 0, filterRequest.ToResponse(), new ServiceMessages().AddNotFound(nameof(Bandname), filterRequest.BandnameId));
                }
                else
                {
                    filterResult.BandnameDataResponse = bandnameDataResponse;
                }
            }

            if (filterRequest.ImageId > 0)
            {
                if (await dbContext.Images
                .IncludeAll()
                .ToResponses()
                .FirstOrDefaultAsync(i => i.Id == filterRequest.ImageId) is not { } imageDataResponse)
                {
                    return ((IList<PerformanceDataResponse>?)null).Wrap(paginationRequest, 0, filterRequest.ToResponse(), new ServiceMessages().AddNotFound(nameof(Image), filterRequest.ImageId));
                }
                else
                {
                    filterResult.ImageDataResponse = imageDataResponse;
                }
            }

            var totalCount = await dbContext.Performances
                .IncludeAll()
                .Filter(filterRequest)
                .CountAsync();

            return (await dbContext.Performances
                .IncludeAll()
                .Filter(filterRequest)
                .GetPage(paginationRequest)
                .ToResponses()
                .ToListAsync())
                .Wrap(paginationRequest, totalCount, filterRequest.ToResponse());
        }

        public async Task<ServiceResult<PerformanceDataResponse>> Update(int id, PerformanceDataRequest request)
        {
            if (await dbContext.Performances.IncludeAll().FirstOrDefaultAsync(p => p.Id == id) is not { } performance)
            {
                return ((PerformanceDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Performance), id).AddNotUpdated(nameof(Performance), id));
            }


            if (!await dbContext.Acts.ExistsOrIsNull(request.ActId))
            {
                return ((PerformanceDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Act), id).AddNotUpdated(nameof(Performance), id));
            }

            if (!await dbContext.Artists.ExistsOrIsNull(request.ArtistId))
            {
                return ((PerformanceDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Artist), request.ArtistId).AddNotUpdated(nameof(Performance), id));
            }

            if (!await dbContext.Bandnames.ExistsOrIsNull(request.BandnameId))
            {
                return ((PerformanceDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Bandname), request.BandnameId).AddNotUpdated(nameof(Performance), id));
            }

            if (!await dbContext.Images.ExistsOrIsNull(request.ImageId))
            {
                return ((PerformanceDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Image), request.ImageId).AddNotUpdated(nameof(Performance), id));
            }

            performance.Update(request);

            await dbContext.SaveChangesAsync();

            return performance.ToResponse().Wrap();
        }
    }
}
