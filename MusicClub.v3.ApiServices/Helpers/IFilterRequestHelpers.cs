namespace MusicClub.v3.ApiServices.Helpers
{
    public interface IFilterRequestHelpers<TFilterRequest, TFilterResult>
    {
        string ToQueryString(TFilterRequest filterRequest);

        TFilterResult ToResult(TFilterRequest filterRequest);
    }
}
