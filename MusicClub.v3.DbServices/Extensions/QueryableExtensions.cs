using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.DbServices.Extensions
{
    internal static class QueryableExtensions
    {
        public static IQueryable<T> GetPage<T>(this IQueryable<T> query, PaginationRequest paginationRequest)
        {           
            return query.Skip((paginationRequest.Page - 1) * paginationRequest.PageSize).Take(paginationRequest.PageSize);
        } 
    }
}