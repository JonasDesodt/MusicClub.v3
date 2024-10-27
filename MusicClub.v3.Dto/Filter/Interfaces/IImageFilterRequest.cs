namespace MusicClub.v3.Dto.Filter.Interfaces
{
    //todo => add exclude properties param string[] to the GenerateFilterRequest/Response-Attributes ?? allong w/ the IImage interface
    internal interface IImageFilterRequest
    {
        string Alt { get; set; }

        string? ContentType { get; set; }
    }
}
