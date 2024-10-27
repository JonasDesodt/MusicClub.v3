namespace MusicClub.v3.IModels
{
    public interface ILineup
    {
        string? Title { get; set; }
        DateTime Doors { get; set; }
        int? ImageId { get; set; }
    }
}
