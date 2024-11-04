using MusicClub.v3.DbCore.Models;
using MusicClub.v3.DbCore;
using MusicClub.v3.DbServices.Extensions;
using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Mappers.Filter.Request;
using MusicClub.v3.Dto.Transfer;
using MusicClub.v3.DbServices.Extensions.Artist;
using Microsoft.EntityFrameworkCore;
using MusicClub.v3.DbServices.Extensions.Performance;
using MusicClub.v3.DbCore.Mappers.IModel;

namespace MusicClub.v3.DbServices
{
    public class ArtistDbService(MusicClubDbContext dbContext) : IArtistService
    {
        public async Task<ServiceResult<ArtistDataResponse>> Create(ArtistDataRequest request)
        {
            if (!await dbContext.People.ExistsOrIsNull(request.PersonId))
            {
                return ((ArtistDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Person), request.PersonId).AddNotCreated(nameof(Artist)));
            }

            if (!await dbContext.Images.ExistsOrIsNull(request.ImageId))
            {
                return ((ArtistDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Image), request.ImageId).AddNotCreated(nameof(Artist)));
            }

            var artist = request.ToModel(dbContext.CurrentTenantId);

            await dbContext.Artists.AddAsync(artist);

            await dbContext.SaveChangesAsync();

            return await Get(artist.Id);
        }

        public async Task<ServiceResult<ArtistDataResponse>> Delete(int id)
        {
            if (await dbContext.Artists.FindAsync(id) is not { } artist)
            {
                return ((ArtistDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Artist), id).AddNotDeleted(nameof(Artist), id));
            }

            if (await dbContext.Performances.HasReferenceToArtist(id))
            {
                return ((ArtistDataResponse?)null).Wrap(new ServiceMessages().AddReferenceFound(nameof(Artist), id, nameof(Performance)).AddNotDeleted(nameof(Artist), id));
            }

            dbContext.Artists.Remove(artist);

            await dbContext.SaveChangesAsync();

            return ((ArtistDataResponse?)null).Wrap();
        }

        public Task<ServiceResult<bool>> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<ArtistDataResponse>> Get(int id)
        {
            return (await dbContext.Artists
                    .IncludeAll()
                    .ToResponses()
                    .FirstOrDefaultAsync(p => p.Id == id))
                    .Wrap(new ServiceMessages().AddNotFound(nameof(Artist), id));
        }

        public async Task<PagedServiceResult<IList<ArtistDataResponse>, ArtistFilterResponse>> GetAll(PaginationRequest paginationRequest, ArtistFilterRequest filterRequest)
        {
            var totalCount = await dbContext.Artists
                .IncludeAll()
                .Filter(filterRequest)
                .CountAsync();

            return (await dbContext.Artists
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

        public async Task<ServiceResult<ArtistDataResponse>> Update(int id, ArtistDataRequest request)
        {
            if (await dbContext.Artists.IncludeAll().FirstOrDefaultAsync(p => p.Id == id) is not { } artist)
            {
                return ((ArtistDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Artist), id).AddNotUpdated(nameof(Artist), id));
            }

            if (!await dbContext.People.ExistsOrIsNull(request.PersonId))
            {
                return ((ArtistDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Person), request.PersonId).AddNotUpdated(nameof(Artist), id));
            }

            if (!await dbContext.Images.ExistsOrIsNull(request.ImageId))
            {
                return ((ArtistDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Image), request.ImageId).AddNotUpdated(nameof(Artist), id));
            }

            artist.Update(request);

            await dbContext.SaveChangesAsync();

            return artist.ToResponse().Wrap();
        }
    }
}
