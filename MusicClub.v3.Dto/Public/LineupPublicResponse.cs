namespace MusicClub.v3.Dto.Public
{
    public class LineupPublicResponse
    {
        public required int Id { get; set; }
        public string? Title { get; set; }
        public required DateTime Doors { get; set; }

        public ImagePublicResponse? Image { get; set; }

        public IList<ActPublicResponse> Acts { get; set; } = [];
        public required int ActsPage { get; set; }
        public required int ActsPageSize { get; set; }
        public required int ActsTotalCount { get; set; }

    }
}
