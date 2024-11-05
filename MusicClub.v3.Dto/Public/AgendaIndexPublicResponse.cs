namespace MusicClub.v3.Dto.Public
{
    public class AgendaIndexPublicResponse
    {
        public IList<LineupPublicResponse> Lineups { get; set; } = [];

        public required int Page { get; set; }
        public required int PageSize { get; set;  }
        public required int TotalCount { get; set; }

        public string? Search { get; set; }
        public DateTime? From { get; set; }
        public DateTime? Until { get; set; }
    }
}
