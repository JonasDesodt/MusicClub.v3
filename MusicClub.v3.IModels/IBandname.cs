namespace MusicClub.v3.IModels
{
    public interface IBandname
    {
        string Name { get; set; }
        int? ImageId { get; set; }
        int BandId { get; set; }

    }
}
