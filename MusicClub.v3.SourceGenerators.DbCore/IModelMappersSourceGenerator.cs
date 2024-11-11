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

            foreach (var (classDeclarationSyntax, symbol, attributeData) in receiver.GetClassDeclarationSyntaxWithAttributeData(context, "GenerateIModelMappers"))
            {
                var modelType = symbol.GetClassName();

                var constants = attributeData.GetConstants(new string[]
                {
                    "InterfacePrefix",
                    "NamespaceReplacePattern",
                    "NamespaceReplacement",
                    "Created",
                    "Updated",
                    "TenantId"
                });

                var interfacePrefix = constants["InterfacePrefix"];

                var interfaceType = interfacePrefix + modelType;

                var namespaceReplacePattern = constants["NamespaceReplacePattern"];

                var namespaceReplacement = constants["NamespaceReplacement"];

                if (!new Regex(namespaceReplacePattern).TryReplace(symbol.GetNamespace(), namespaceReplacement, out string @namespace))
                {
                    continue;
                }

                var properties = symbol.GetInterfaceProperties().Select(x => x.Name);

                if (!(attributeData.GetParamStringArrayValues() is IEnumerable<string> additionalProperties))
                {
                    continue;
                }

                var classname = interfaceType + "Extensions"; //get extension from the attribute?

                var created = constants["Created"];

                var updated = constants["Updated"];

                var tenantId = constants["TenantId"];

                var source = ClassStrings.GetIModelToModelString(@namespace, classname, modelType, properties, additionalProperties, interfaceType, created, updated, tenantId);

                context.AddSource(classname + NamingConventions.FileExtension, source);
            }
        }
    }
}
