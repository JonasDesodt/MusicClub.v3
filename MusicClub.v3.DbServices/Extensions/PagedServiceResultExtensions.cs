using MusicClub.v3.Dto.Extensions;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.DbServices.Extensions
{
    internal static class PagedServiceResultExtensions
    {
        public static PagedServiceResult<IList<TDataResult>, TFilterResult> Wrap<TDataResult, TFilterResult>(this IList<TDataResult>? data, PaginationRequest paginationRequest, int totalCount,TFilterResult filter, ServiceMessages? messages = null) 
        {
            return new PagedServiceResult<IList<TDataResult>, TFilterResult>
            {
                Data = data,
                Messages = data == null ? messages : null,
                PaginationResponse = paginationRequest.ToResponse(totalCount),
                Filter = filter
            };
        }
    }
}