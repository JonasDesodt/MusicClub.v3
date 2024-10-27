using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.DbServices
{
    public class PersonDbService : IPersonService
    {
        public Task<ServiceResult<PersonDataResponse>> Create(PersonDataRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<PersonDataResponse>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<PersonDataResponse>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedServiceResult<IList<PersonDataResponse>, PersonFilterResponse>> GetAll(PaginationRequest paginationRequest, PersonFilterRequest filterRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<PersonDataResponse>> Update(int id, PersonDataRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
