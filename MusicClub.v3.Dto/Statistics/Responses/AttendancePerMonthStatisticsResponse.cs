namespace MusicClub.v3.Dto.Statistics.Responses
{
    public class AttendancePerMonthStatisticsResponse
    {
        public required DateTime From { get; set; }

        public required DateTime Until { get; set; }


        public IDictionary<int, int> Statistics { get; set; } = new Dictionary<int, int>();
    }
}
