namespace MusicClub.v3.Dto.Transfer
{
    public class PagedServiceResult<TModel, TFilter> : ServiceResult<TModel>
    {
        public required PaginationResponse PaginationResponse {get;set;}

        public required TFilter Filter { get; set; }
    }
}