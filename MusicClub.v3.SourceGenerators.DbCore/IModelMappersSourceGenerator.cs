using Microsoft.CodeAnalysis;
using MusicClub.v3.SourceGenerators.Shared.Constants;
using MusicClub.v3.SourceGenerators.Shared.Extensions;
using MusicClub.v3.SourceGenerators.Shared.Receivers;
using MusicClub.v3.SourceGenerators.Shared.Strings;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MusicClub.v3.SourceGenerators.DbCore
{
    [Generator]
    internal class IModelMappersSourceGenerator : ISourceGenerator
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
            foreach (var (classDeclarationSyntax, attributeData) in receiver.GetClassDeclarationSyntaxWithAttributeData(context.Compilation, "GenerateIModelMappers"))
            {
                var modelType = context.GetClassName(classDeclarationSyntax);

                //if (!(attributeData.GetPropertyValue("DataRequestClassNameSuffix") is string dataRequestTypeSuffix))
                //{
                //    continue;
                //}
                if (!(attributeData.GetPropertyValue("InterfacePrefix") is string interfacePrefix))
                {
                    continue;
                }

                var interfaceType = interfacePrefix + modelType;

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

                //var properties = context.GetPropertySymbolNames(classDeclarationSyntax);
               var properties = context.GetInterfaceProperties(classDeclarationSyntax).Select(x => x.Name);

                if (!(attributeData.GetParamStringArrayValues() is IEnumerable<string> additionalProperties))
                {
                    continue;
                }

                var classname = interfaceType + "Extensions"; //get extension from the attribute?

                if (!(attributeData.GetPropertyValue("Created") is string created))
                {
                    continue;
                }

                if (!(attributeData.GetPropertyValue("Updated") is string updated))
                {
                    continue;
                }

                if (!(attributeData.GetPropertyValue("TenantId") is string tenantId))
                {
                    continue;
                }

                var source = ClassStrings.GetIModelToModelString(@namespace, classname, modelType, properties, additionalProperties, interfaceType, created, updated, tenantId);

                context.AddSource(classname + NamingConventions.FileExtension, source);
            }
        }
    }
}
