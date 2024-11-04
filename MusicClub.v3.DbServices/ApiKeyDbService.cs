using MusicClub.v3.DbCore;
using MusicClub.v3.DbServices.Helpers;
using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Dto.Transfer;
using MusicClub.v3.DbCore.Mappers.IModel;
using MusicClub.v3.DbServices.Extensions;

namespace MusicClub.v3.DbServices
{
    public class ApiKeyDbService(MusicClubDbContext dbContext) : IApiKeyService
    {
        public async Task<ServiceResult<ApiKeyDataResponse>> Create(ApiKeyDataRequest request)
        {
            (string apiKey, byte[] hashedApiKey, byte[] salt) = ApiKeyHelper.GenerateApiKey();

            var apiKeyRecord = request.ToModel(hashedApiKey, salt);

            //todo => check if the key not already in the db
            await dbContext.ApiKeys.AddAsync(apiKeyRecord);

            //todo => set all the non archived api keys as archived

            await dbContext.SaveChangesAsync();

            return new ApiKeyDataResponse { ApiKey = apiKey }.Wrap(); // todo => return the api key according to protocol
        }

        public Task<ServiceResult<ApiKeyDataResponse>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<ApiKeyDataResponse>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedServiceResult<IList<ApiKeyDataResponse>, ApiKeyFilterResponse>> GetAll(PaginationRequest paginationRequest, ApiKeyFilterRequest filterRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<ApiKeyDataResponse>> Update(int id, ApiKeyDataRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
