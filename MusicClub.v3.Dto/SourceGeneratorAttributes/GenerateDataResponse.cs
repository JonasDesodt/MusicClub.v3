namespace MusicClub.v3.Dto.SourceGeneratorAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class GenerateDataResponse : Attribute 
    {
        public const string ClassNamePattern = @$"(?<=\S){GeneratorConstants.Data}{GeneratorConstants.Request}(?!.+{GeneratorConstants.Data}{GeneratorConstants.Request})";
        public const string ClassNameReplacement = GeneratorConstants.Data + GeneratorConstants.Response;

        public const string NamespacePattern = @$"(?<=\S){GeneratorConstants.Request}(?!.+{GeneratorConstants.Request})";
        public const string NamespaceReplacement = GeneratorConstants.Response;

        public const string ForeignKeyPattern = @$"(?<=\S){GeneratorConstants.Id}$"; //@$"(?<=\S){GeneratorConstants.Id}(?!.+{GeneratorConstants.Id})";
        public const string ForeignKeyReplacement = ClassNameReplacement;
    }
}
