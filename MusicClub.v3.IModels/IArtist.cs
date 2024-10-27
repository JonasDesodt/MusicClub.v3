namespace MusicClub.v3.IModels
{
    public interface IArtist
    {
        string? Alias { get; set; }

        int PersonId { get; set; }

        int? ImageId { get; set; }
    }
}
