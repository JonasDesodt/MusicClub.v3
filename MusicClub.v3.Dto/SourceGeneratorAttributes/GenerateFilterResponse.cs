namespace MusicClub.v3.Dto.SourceGeneratorAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class GenerateFilterResponse : Attribute 
    {
        public const string ClassNamePattern = @$"(?<=\S){GeneratorConstants.Filter}{GeneratorConstants.Request}(?!.+{GeneratorConstants.Filter}{GeneratorConstants.Request})";
        public const string ClassNameReplacement = GeneratorConstants.Filter + GeneratorConstants.Response;

        public const string NamespacePattern = @$"(?<=\S){GeneratorConstants.Request}(?!.+{GeneratorConstants.Request})";
        public const string NamespaceReplacement = GeneratorConstants.Response;

        public const string ForeignKeyPattern = @$"(?<=\S){GeneratorConstants.Id}(?!.+{GeneratorConstants.Id})";
        public const string ForeignKeyReplacement = GeneratorConstants.Data + GeneratorConstants.Response;
    }
}
