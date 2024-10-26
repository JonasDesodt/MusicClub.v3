using System.Collections.Immutable;

namespace MusicClub.v3.DbCore.SourceGeneratorAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class GenerateIModelMappers(params string[] additionalProperties) : Attribute
    {
        public const string Created = SourceGeneratorConstants.Created;
        public const string Updated = SourceGeneratorConstants.Updated;

        public const string DataRequestClassNameSuffix = SourceGeneratorConstants.Data + SourceGeneratorConstants.Request;
        public const string InterfacePrefix = "I";

        public const string NamespaceReplacePattern = @$"(?<=\S){SourceGeneratorConstants.Models}(?!.+{SourceGeneratorConstants.Models})";
        public const string NamespaceReplacement = SourceGeneratorConstants.Mappers + "." + "IModel";
    }
}
