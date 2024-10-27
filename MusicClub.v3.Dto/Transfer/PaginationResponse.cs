namespace MusicClub.v3.Dto.Transfer
{
    public class PaginationResponse 
    {
        public required int Page { get; set; }

        public required int PageSize { get; set; }

        public required int TotalCount { get; set; }
    }
}