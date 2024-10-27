namespace MusicClub.v3.Dto.Data.Response
{
    public class ImageDataResponse
    {
        public required int Id { get; set; }

        public required string Alt { get; set; }
        public required string ContentType { get; set; }

        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }

        public required int ArtistsCount { get; set; }
        public required int ActsCount { get; set; }
        public required int PeopleCount { get; set; }
        public required int PerformancesCount { get; set; }
        public required int LineupsCount { get; set; }
    }
}
