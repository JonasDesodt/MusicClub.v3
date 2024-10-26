namespace MusicClub.v3.Dto.SourceGeneratorAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class GenerateFilterMappers : Attribute
    {
        public const string Request = GeneratorConstants.Request;
        public const string Response = GeneratorConstants.Response;

        public const string ValidationPattern = @$".+{GeneratorConstants.Filter}{Request}$";

        public const string ClassNameReplacePattern = @$"(?<=\S){Request}(?!.+{Request})";
        public const string ClassNameReplacement = Response;
        public const string ClassNameSuffix = GeneratorConstants.Mappers;

        public const string ForeignKeyReplacePattern = @$"(?<=\S){GeneratorConstants.Id}(?!.+{GeneratorConstants.Id})";
        public const string ForeignKeyReplacement = GeneratorConstants.Data + ClassNameReplacement;

        public const string NamespaceReplacePattern = @$"(?<=\S){GeneratorConstants.Filter}\.{Request}(?!.+{GeneratorConstants.Filter}\.{Request})";
        public const string NamespaceReplacement = GeneratorConstants.Mappers + "." + GeneratorConstants.Filter;
    }
}
