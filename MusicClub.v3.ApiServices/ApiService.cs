using MusicClub.v3.Dto.Transfer;
using MusicClub.v3.Abstractions.Services;
using MusicClub.v3.ApiServices.Extensions;
using MusicClub.v3.ApiServices.Helpers;
using MusicClub.v3.ApiServices.SourceGeneratorAttributes;
using MusicClub.v3.Dto.Helpers;

namespace MusicClub.v3.ApiServices
{
    [GenerateApiServices( // todo => use the IModels ?
        "Act", 
        "Artist", 
        "Band", 
        "Bandname", 
        "Description", 
        "DescriptionTranslation", 
        "Function", 
        "Job", 
        "Language", 
        "Lineup",
        "Performance",
        "Person",
        "Service",
        "Worker")]
    public abstract class ApiService<TDataRequest, TDataResult, TFilterRequest, TFilterResponse>(IHttpClientFactory httpClientFactory, IFilterRequestHelpers<TFilterRequest, TFilterResponse> filterRequestHelpers) : IService<TDataRequest, TDataResult, TFilterRequest, TFilterResponse> where TFilterResponse : new() //<= todo, temp hack, can be removed when the filterequesthelpers implement filterrequest toResponse()
    {
        protected abstract string Endpoint { get; }

        public async Task<ServiceResult<TDataResult>> Create(TDataRequest request)
        {
            return await httpClientFactory.Create<TDataRequest, TDataResult>("MusicClubApi", $"{Endpoint}/", request);
        }

        public async Task<ServiceResult<TDataResult>> Delete(int id)
        {
            return await httpClientFactory.Delete<TDataResult>("MusicClubApi", $"{Endpoint}/", id);
        }

        public Task<ServiceResult<bool>> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<TDataResult>> Get(int id)
        {
            return await httpClientFactory.Get<TDataResult>("MusicClubApi", $"{Endpoint}/", id);
        }

        public async Task<PagedServiceResult<IList<TDataResult>, TFilterResponse>> GetAll(PaginationRequest paginationRequest, TFilterRequest filterRequest)
        {
            return await httpClientFactory.GetAll<TDataResult, TFilterRequest, TFilterResponse>(filterRequestHelpers, "MusicClubApi", $"{Endpoint}?", paginationRequest, filterRequest);

        }

        public Task<ServiceResult<bool>> IsReferenced(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<TDataResult>> Update(int id, TDataRequest request)
        {
            return await httpClientFactory.Update<TDataRequest, TDataResult>("MusicClubApi", $"{Endpoint}/", id, request);

        }
    }
}
