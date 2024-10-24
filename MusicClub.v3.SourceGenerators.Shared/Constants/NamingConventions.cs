using System.Text.RegularExpressions;

namespace MusicClub.v3.SourceGenerators.Shared.Constants
{
    public static class NamingConventions
    {
        public const string FileExtension = ".g.cs";

        public const string DataRequestSuffix = "DataRequest";
        public const string DataResponseSuffix = "DataResponse";
        public const string FilterRequestSuffix = "FilterRequest";
        public const string FilterResponseSuffix = "FilterResponse";

        public static string[] GetDtoSuffixes() => new string[] { DataRequestSuffix, DataResponseSuffix, FilterRequestSuffix, FilterResponseSuffix };

        public const string DbServiceSuffix = "DbService";

        public static Regex IModelServicePattern { get; } = new Regex(@"^I.+Service$");
    }
}
