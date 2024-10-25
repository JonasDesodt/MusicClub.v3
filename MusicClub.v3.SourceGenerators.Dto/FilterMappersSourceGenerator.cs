using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MusicClub.v3.SourceGenerators.Shared.Constants;
using MusicClub.v3.SourceGenerators.Shared.Extensions;
using MusicClub.v3.SourceGenerators.Shared.Receivers;
using MusicClub.v3.SourceGenerators.Shared.Strings;
using System.Text.RegularExpressions;

namespace MusicClub.v3.SourceGenerators.Dto
{
    [Generator]
    internal class FilterMappersSourceGenerator : ISourceGenerator
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


            foreach (var (requestClassDeclarationSyntax, attributeData) in receiver.GetClassDeclarationSyntaxWithAttributeData(context.Compilation, "GenerateFilterMappers"))
            {
                if (!(attributeData.GetPropertyValue("ValidationPattern") is string validationPattern))
                {
                    continue;
                }

                var filterRequestTypeName = context.GetClassName(requestClassDeclarationSyntax);

                if (!Regex.IsMatch(filterRequestTypeName, validationPattern))
                {
                    continue;
                }

                if (!(attributeData.GetPropertyValue("ClassNameReplacePattern") is string classNameReplacePattern))
                {
                    continue;
                }
                if (!(attributeData.GetPropertyValue("ClassNameReplacement") is string classNameReplacement))
                {
                    continue;
                }

                var filterResponseTypeName = Regex.Replace(filterRequestTypeName, classNameReplacePattern, classNameReplacement);

                if (!(attributeData.GetPropertyValue("ClassNameSuffix") is string classNameSuffix))
                {
                    continue;
                }

                var filterRequestExtensionsClassname = filterRequestTypeName + classNameSuffix;
                var filterResponseExtensionsClassName = filterResponseTypeName + classNameSuffix;

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
                if (!(attributeData.GetPropertyValue("Response") is string response))
                {
                    continue;
                }

                var filterRequestExtensionsNamespace = baseNamespace + "." + request;
                var filterResponseExtensionsNamespace = baseNamespace + "." + response;
                var properties = context.GetPropertySymbols(requestClassDeclarationSyntax);

                context.AddSource(filterRequestExtensionsClassname + NamingConventions.FileExtension, "//" + filterRequestExtensionsNamespace);
                context.AddSource(filterResponseExtensionsClassName + NamingConventions.FileExtension, ClassStrings.GetResponseToRequestMapperString(filterResponseExtensionsNamespace, filterResponseExtensionsClassName, filterResponseTypeName, filterRequestTypeName, properties));
            }
        }
    }
}
