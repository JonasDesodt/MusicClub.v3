namespace MusicClub.v3.Dto.SourceGeneratorAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class GenerateDataResponse : Attribute 
    {
        public const string ClassNamePattern = @"(?<=\S)DataRequest(?!.+DataRequest)";
        public const string ClassNameReplacement = "DataResponse";

        public const string NamespacePattern = @"(?<=\S)Request(?!.+Request)";
        public const string NamespaceReplacement = "Response";
    }
}
