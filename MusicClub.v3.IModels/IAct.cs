namespace MusicClub.v3.IModels
{
    public interface IAct
    {
        string Name { get; set; }
        string? Title { get; set; }
        string? Description { get; set; }

        DateTime? Start { get; set; }
        int? Duration { get; set; }

        int? ImageId { get; set; }
        int LineupId { get; set; }
        int? GoogleEventId { get; set; }
    }
}
