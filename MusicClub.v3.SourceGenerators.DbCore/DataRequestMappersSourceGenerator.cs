using Microsoft.CodeAnalysis;
using MusicClub.v3.SourceGenerators.Shared.Constants;
using MusicClub.v3.SourceGenerators.Shared.Extensions;
using MusicClub.v3.SourceGenerators.Shared.Receivers;
using MusicClub.v3.SourceGenerators.Shared.Strings;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MusicClub.v3.SourceGenerators.DbCore
{
    [Generator]
    internal class DataRequestMappersSourceGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new ClassDeclarationSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (!(context.SyntaxReceiver is ClassDeclarationSyntaxReceiver receiver))
            {
                return;
            }
            foreach (var (classDeclarationSyntax, attributeData) in receiver.GetClassDeclarationSyntaxWithAttributeData(context.Compilation, "GenerateDataRequestMappers"))
            {
                var modelType = context.GetClassName(classDeclarationSyntax);

                if (!(attributeData.GetPropertyValue("DataRequestClassNameSuffix") is string dataRequestTypeSuffix))
                {
                    continue;
                }

                var dataRequestType = modelType + dataRequestTypeSuffix;

                if (!(attributeData.GetPropertyValue("NamespaceReplacePattern") is string namespaceReplacePattern))
                {
                    continue;
                }

                if (!(attributeData.GetPropertyValue("NamespaceReplacement") is string namespaceReplacement))
                {
                    continue;
                }

                if (!new Regex(namespaceReplacePattern).TryReplace(context.GetNamespace(classDeclarationSyntax), namespaceReplacement, out string @namespace))
                {
                    continue;
                }

                var properties = context.GetPropertySymbolNames(classDeclarationSyntax);

                if (!(attributeData.GetParamStringArrayValues() is IEnumerable<string> excludedProperties))
                {
                    continue;
                }

                var classname = dataRequestType + "Extensions"; //get extension from the attribute?

                if (!(attributeData.GetPropertyValue("Created") is string created))
                {
                    continue;
                }

                if (!(attributeData.GetPropertyValue("Updated") is string updated))
                {
                    continue;
                }

                var source = ClassStrings.GetDataRequestToModelString(@namespace, classname, modelType, properties, excludedProperties, dataRequestType, created, updated);

                context.AddSource(classname + NamingConventions.FileExtension, source);
            }
        }
    }
}
