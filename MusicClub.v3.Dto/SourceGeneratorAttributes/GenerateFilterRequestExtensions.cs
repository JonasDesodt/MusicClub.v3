namespace MusicClub.v3.Dto.SourceGeneratorAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class GenerateFilterRequestExtensions(Type iModel) : Attribute
    {
        public const string Request = GeneratorConstants.Request;

        public const string ValidationPattern = @$".+{GeneratorConstants.Filter}{Request}$";

        public const string ClassNameSuffix = GeneratorConstants.Extensions;

        public const string NamespaceReplacePattern = @$"(?<=\S){GeneratorConstants.Filter}\.{Request}(?!.+{GeneratorConstants.Filter}\.{Request})";
        public const string NamespaceReplacement = GeneratorConstants.Extensions + "." + GeneratorConstants.Filter;
    }
}
