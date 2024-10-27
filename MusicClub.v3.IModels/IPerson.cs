namespace MusicClub.v3.IModels
{ 
    public interface IPerson
    {
        string Firstname { get; set; }
        string Lastname { get; set; }
        int? ImageId { get; set; }
    }
}
