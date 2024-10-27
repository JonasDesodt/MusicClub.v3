using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.Dto.Extensions
{
    public static class PaginationRequestExtensions
    {
        public static PaginationResponse ToResponse(this PaginationRequest request, int totalCount)
        {
            return new PaginationResponse
            {
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount,
            };
        }
    }
}
