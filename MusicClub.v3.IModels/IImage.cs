namespace MusicClub.v3.IModels
{
    public interface IImage
    {
        string Alt { get; set; }
        byte[] Content { get; set; }
        string ContentType { get; set; }
    }
}
