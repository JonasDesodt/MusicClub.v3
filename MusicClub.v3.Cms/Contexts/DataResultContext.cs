namespace MusicClub.v3.Cms.Contexts
{
    public class DataResultContext<TDataResponse> where TDataResponse : class
    {
        public required TDataResponse DataResult { get; set; }

        public bool ShowImages { get; set; } = true;
    }
}
