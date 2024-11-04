using Microsoft.EntityFrameworkCore;
using MusicClub.v3.DbCore;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Transfer;
using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.DbServices.Extensions.Image;
using MusicClub.v3.DbServices.Extensions;
using MusicClub.v3.DbCore.Models;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Mappers.Filter.Request;

namespace MusicClub.v3.DbServices
{
    public class ImageDbService(MusicClubDbContext dbContext) : IImageDbService
    {
        public async Task<ServiceResult<ImageDataResponse>> Create(ImageDbDataRequest request)
        {
            var image = request.ToModel(dbContext.CurrentTenantId);

            if (image is null)
            {
                return ((ImageDataResponse?)null).Wrap(new ServiceMessages().AddNotCreated(nameof(Image)));
            }

            await dbContext.Images.AddAsync(image);

            await dbContext.SaveChangesAsync();

            return await Get(image.Id);
        }

        public async Task<ServiceResult<ImageDataResponse>> Delete(int id)
        {
            if (await dbContext.Images.FindAsync(id) is not { } image)
            {
                return ((ImageDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Image), id).AddNotDeleted(nameof(Image), id));
            }

            dbContext.Images.Remove(image);

            await dbContext.SaveChangesAsync();

            return ((ImageDataResponse?)null).Wrap();
        }

        public async Task<ServiceResult<bool>> Exists(int id)
        {
            return (await dbContext.Images.FindAsync(id) is not null).Wrap();
        }

        public async Task<ServiceResult<ImageDataResponse>> Get(int id)
        {
            return (await dbContext.Images
                .IncludeAll()
                .ToResponses()
                .FirstOrDefaultAsync(p => p.Id == id))
                .Wrap(new ServiceMessages().AddNotFound(nameof(Person), id));
        }

        public async Task<PagedServiceResult<IList<ImageDataResponse>, ImageFilterResponse>> GetAll(PaginationRequest paginationRequest, ImageFilterRequest filterRequest)
        {
            var totalCount = await dbContext.Images
                .IncludeAll()
                .Filter(filterRequest)
                .CountAsync();

            return (await dbContext.Images
                .IncludeAll()
                .Filter(filterRequest)
                .GetPage(paginationRequest)
                .ToResponses()
                .ToListAsync())
                .Wrap(paginationRequest, totalCount, filterRequest.ToResponse());
        }

        public async Task<ServiceResult<ImageDataResponse>> Update(int id, ImageDbDataRequest request)
        {
            if (await dbContext.Images.FirstOrDefaultAsync(p => p.Id == id) is not { } image)
            {
                return ((ImageDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Image), id).AddNotUpdated(nameof(Image), id));
            }

            image.Update(request);

            await dbContext.SaveChangesAsync();

            return image.ToResponse().Wrap();
        }
    }
}
