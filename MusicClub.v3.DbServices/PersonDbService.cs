using MusicClub.v3.DbCore.Models;
using MusicClub.v3.DbCore;
using MusicClub.v3.DbServices.Extensions;
using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.DbCore.Mappers.IModel;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;
using MusicClub.v3.Dto.Mappers.Filter.Request;
using MusicClub.v3.DbServices.Extensions.Person;
using Microsoft.EntityFrameworkCore;
using MusicClub.v3.DbServices.Extensions.Worker;
using MusicClub.v3.DbServices.Extensions.Artist;
using MusicClub.v3.DbServices.Extensions.Image;

namespace MusicClub.v3.DbServices
{
    public class PersonDbService(MusicClubDbContext dbContext) : IPersonService
    {

        public async Task<ServiceResult<PersonDataResponse>> Create(PersonDataRequest request)
        {
            if (!await dbContext.Images.ExistsOrIsNull(request.ImageId))
            {
                return ((PersonDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Image), request.ImageId).AddNotCreated(nameof(Person)));
            }

            var person = request.ToModel();

            await dbContext.People.AddAsync(person);

            await dbContext.SaveChangesAsync();

            return await Get(person.Id);
        }

        public async Task<ServiceResult<PersonDataResponse>> Delete(int id)
        {
            if (await dbContext.People.FindAsync(id) is not { } person)
            {
                return ((PersonDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Person), id).AddNotDeleted(nameof(Person), id));
            }

            if (await dbContext.Artists.HasReferenceToPerson(id))
            {
                return ((PersonDataResponse?)null).Wrap(new ServiceMessages().AddReferenceFound(nameof(Person), id, nameof(Artist)).AddNotDeleted(nameof(Person), id));
            }

            if (await dbContext.Workers.HasReferenceToPerson(id))
            {
                return ((PersonDataResponse?)null).Wrap(new ServiceMessages().AddReferenceFound(nameof(Person), id, nameof(Worker)).AddNotDeleted(nameof(Person), id));
            }

            dbContext.People.Remove(person);

            await dbContext.SaveChangesAsync();

            return ((PersonDataResponse?)null).Wrap();
        }

        public async Task<ServiceResult<bool>> Exists(int id)
        {
            return (await dbContext.People.FindAsync(id) is not null).Wrap();
        }

        //todo: hide people who are only applicationusers?
        public async Task<ServiceResult<PersonDataResponse>> Get(int id)
        {
            return (await dbContext.People
                .IncludeAll()
                .ToResponses()
                .FirstOrDefaultAsync(p => p.Id == id))
                .Wrap(new ServiceMessages().AddNotFound(nameof(Person), id));
        }

        //todo: hide people who are only applicationusers?
        public async Task<PagedServiceResult<IList<PersonDataResponse>, PersonFilterResponse>> GetAll(PaginationRequest paginationRequest, PersonFilterRequest filterRequest)
        {
            var filterResponse = filterRequest.ToResponse();

            if (filterRequest.ImageId > 0)
            {
                if (await dbContext.Images
                .IncludeAll()
                .ToResponses()
                .FirstOrDefaultAsync(i => i.Id == filterRequest.ImageId) is not { } imageDataResponse)
                {
                    return ((IList<PersonDataResponse>?)null).Wrap(paginationRequest, 0, filterRequest.ToResponse(), new ServiceMessages().AddNotFound(nameof(Image), filterRequest.ImageId));
                }
                else
                {
                    filterResponse.ImageDataResponse = imageDataResponse;
                }
            }

            var totalCount = await dbContext.People
                .IncludeAll()
                .Filter(filterRequest)
                .CountAsync();

            return (await dbContext.People
                .IncludeAll()
                .Filter(filterRequest)
                .GetPage(paginationRequest)
                .ToResponses()
                .ToListAsync())
                .Wrap(paginationRequest, totalCount, filterResponse);
        }

        public async Task<ServiceResult<bool>> IsReferenced(int id)
        {
            if (await dbContext.Artists.HasReferenceToPerson(id))
            {
                return true.Wrap();
            }

            if (await dbContext.Workers.HasReferenceToPerson(id))
            {
                return true.Wrap();
            }

            return false.Wrap();
        }

        public async Task<ServiceResult<PersonDataResponse>> Update(int id, PersonDataRequest request)
        {
            if (await dbContext.People.IncludeAll().FirstOrDefaultAsync(p => p.Id == id) is not { } person)
            {
                return ((PersonDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Person), id).AddNotUpdated(nameof(Person), id));
            }

            if (!await dbContext.Images.ExistsOrIsNull(request.ImageId))
            {
                return ((PersonDataResponse?)null).Wrap(new ServiceMessages().AddNotFound(nameof(Image), request.ImageId).AddNotUpdated(nameof(Person), id));
            }

            person.Update(request);

            await dbContext.SaveChangesAsync();

            return person.ToResponse().Wrap();
        }
    }
}
