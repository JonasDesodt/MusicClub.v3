using MusicClub.v3.Abstractions.SourceGeneratorAttributes;

namespace MusicClub.v3.Abstractions.Services
{
    [GenerateIServices("Person")]
    public interface IService<TDataRequest, TDataResponse, TFilterRequest, TFilterResponse>
    {
        //Task<ServiceResult<TDataResult>> Create(TDataRequest request);

        //Task<ServiceResult<TDataResult>> Get(int id);

        //Task<PagedServiceResult<IList<TDataResult>, TFilterResult>> GetAll(PaginationRequest paginationRequest, TFilterRequest filterRequest);

        //Task<ServiceResult<TDataResult>> Delete(int id);

        //Task<ServiceResult<TDataResult>> Update(int id, TDataRequest request);
    }
}
