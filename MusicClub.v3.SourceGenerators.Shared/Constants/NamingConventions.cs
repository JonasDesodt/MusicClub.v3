namespace MusicClub.v3.SourceGenerators.Shared.Constants
{
    public static class NamingConventions
    {
        public const string FileExtension = ".g.cs";

        public const string DataRequest = "DataRequest";
        public const string DataResponse = "DataResponse";
        public const string FilterRequest = "FilterRequest";
        public const string FilterResponse = "FilterResponse";

        public static string[] GetDto() => new string[] { DataRequest, DataResponse, FilterRequest, FilterResponse };
    }
}
