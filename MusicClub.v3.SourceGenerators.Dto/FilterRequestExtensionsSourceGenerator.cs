using Microsoft.CodeAnalysis;
using MusicClub.v3.SourceGenerators.Shared.Constants;
using MusicClub.v3.SourceGenerators.Shared.Extensions;
using MusicClub.v3.SourceGenerators.Shared.Receivers;
using MusicClub.v3.SourceGenerators.Shared.Strings;
using System.Linq;
using System.Text.RegularExpressions;

namespace MusicClub.v3.SourceGenerators.Dto
{
    [Generator]
    internal class FilterRequestExtensionsSourceGenerator : ISourceGenerator
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

            foreach (var (requestClassDeclarationSyntax, attributeData) in receiver.GetClassDeclarationSyntaxWithAttributeData(context.Compilation, "GenerateFilterRequestExtensions"))
            {
                if (!(attributeData.GetPropertyValue("ValidationPattern") is string validationPattern))
                {
                    continue;
                }

                if (!(attributeData.GetPropertyValue("ClassNameSuffix") is string classNameSuffix))
                {
                    continue;
                }

                var @type = context.GetClassName(requestClassDeclarationSyntax);
                var @class = @type + classNameSuffix;

                if (!(attributeData.GetPropertyValue("NamespaceReplacePattern") is string namespaceReplacePattern))
                {
                    continue;
                }
                if (!(attributeData.GetPropertyValue("NamespaceReplacement") is string namespaceReplacement))
                {
                    continue;
                }

                if (!new Regex(namespaceReplacePattern).TryReplace(context.GetNamespace(requestClassDeclarationSyntax), namespaceReplacement, out string baseNamespace))
                {
                    continue;
                }
                if (!(attributeData.GetPropertyValue("Request") is string request))
                {
                    continue;
                }

                var @namespace = baseNamespace + "." + request;


                //todo => get the props through the attributeData, now is done through the attributeSyntax
                var interfaceProperties = context.GetInterfacePropertiesFromAttributeConstructorParam(requestClassDeclarationSyntax, "GenerateFilterMappers");
                var properties = context.GetPropertySymbols(requestClassDeclarationSyntax).Concat(interfaceProperties);

                context.AddSource(@class + NamingConventions.FileExtension, ClassStrings.GetToQueryStringExtensionString(@namespace, @class, @type, properties));
            }
        }



    }
}
