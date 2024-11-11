namespace MusicClub.v3.Dto.Statistics.Requests
{
    public class AttendancePerMonthStatisticsRequest
    {
        [Required]
        public required DateTime From { get; set; }

        [Required]
        public required DateTime Until { get; set; }
    }
}
