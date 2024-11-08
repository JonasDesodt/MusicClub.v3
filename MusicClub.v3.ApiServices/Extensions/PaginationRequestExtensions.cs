using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.ApiServices.Extensions
{
    internal static class PaginationRequestExtensions
    {
        public static string ToQueryString(this PaginationRequest paginationRequest)
        {
            return $"page={paginationRequest.Page}&pageSize={paginationRequest.PageSize}";
        }
    }
}