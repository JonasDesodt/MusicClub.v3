namespace MusicClub.v3.IModels
{
    public interface IPerformance
    {
        string Instrument { get; set; }
        int? ImageId { get; set; }
        int ArtistId { get; set; }
        int ActId { get; set; }
        int? BandnameId { get; set; }
    }
}
