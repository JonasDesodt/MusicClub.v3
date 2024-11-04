using MusicClub.v3.Abstractions.SourceGeneratorAttributes;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.Abstractions.Services
{
    [GenerateIServices("Act", "ApiKey", "Artist", "Band", "Bandname", "Description", "DescriptionTranslation", "Function", "GoogleEvent", "GoogleCalendar", "Job", "Language", "Lineup", "Performance", "Person", "Service", "Tenant", "Tenancy", "Worker")]
    public interface IService<TDataRequest, TDataResponse, TFilterRequest, TFilterResponse>
    {
        Task<ServiceResult<TDataResponse>> Create(TDataRequest request);

        Task<ServiceResult<TDataResponse>> Get(int id);

        Task<PagedServiceResult<IList<TDataResponse>, TFilterResponse>> GetAll(PaginationRequest paginationRequest, TFilterRequest filterRequest);

        Task<ServiceResult<TDataResponse>> Delete(int id);

        Task<ServiceResult<TDataResponse>> Update(int id, TDataRequest request);
    }
}
