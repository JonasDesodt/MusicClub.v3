using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace MusicClub.v3.DbCore.SourceGeneratorAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class GenerateIModelMappers(params string[] additionalProperties) : Attribute
    {
        [SuppressMessage("Style", "IDE0052:Remove unread private member", Justification = "The constructor param is used by source generators")]
        private readonly string[] _additionalProperties = additionalProperties;

        public const string Created = SourceGeneratorConstants.Created;
        public const string Updated = SourceGeneratorConstants.Updated;
        public const string TenantId = SourceGeneratorConstants.TenantId;

        public const string DataRequestClassNameSuffix = SourceGeneratorConstants.Data + SourceGeneratorConstants.Request;
        public const string InterfacePrefix = "I";

        public const string NamespaceReplacePattern = @$"(?<=\S){SourceGeneratorConstants.Models}(?!.+{SourceGeneratorConstants.Models})";
        public const string NamespaceReplacement = SourceGeneratorConstants.Mappers + "." + "IModel";
    }
}
