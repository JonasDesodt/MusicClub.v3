using MusicClub.v3.IModels;

namespace MusicClub.v3.Dto.Data.Request
{
    public class ImageDbDataRequest : IImage
    {
        public required string Alt { get; set; }

        //public required byte[]? Content { get; set; }
        public required byte[] Content { get; set; }

        //public required string? ContentType { get; set; }
        public required string ContentType { get; set; }
    }
}
