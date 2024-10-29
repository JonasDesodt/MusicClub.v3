namespace MusicClub.v3.Dto.Data.Response
{
    public partial class GoogleEventDataResponse
    {
        public required GoogleCalendarDataResponse GoogleCalendarDataResponse { get; set; }

        public required ActDataResponse ActDataResponse { get; set; }
    }
}
