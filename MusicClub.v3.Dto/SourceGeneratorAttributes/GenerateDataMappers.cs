namespace MusicClub.v3.Dto.SourceGeneratorAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class GenerateDataMappers : Attribute
    {
        public const string Request = GeneratorConstants.Request;
        public const string Response = GeneratorConstants.Response;

        public const string ValidationPattern = @$".+{GeneratorConstants.Data}{Request}$";

        public const string ClassNameReplacePattern = @$"(?<=\S){Request}(?!.+{Request})";
        public const string ClassNameReplacement = Response;
        public const string ClassNameSuffix = GeneratorConstants.Extensions;

        public const string NamespaceReplacePattern = @$"(?<=\S){GeneratorConstants.Data}\.{Request}(?!.+{GeneratorConstants.Data}\.{Request})";
        public const string NamespaceReplacement = GeneratorConstants.Extensions + "." + GeneratorConstants.Data;    
    }
}
