namespace MusicClub.v3.Dto.Public
{
    public class ActPublicResponse
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Title { get; set; }
        public DateTime? Start { get; set; }
        public int? Duration { get; set; }
        public string? Description { get; set; }
        public ImagePublicResponse? Image { get; set; }
    }
}
