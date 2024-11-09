using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MusicClub.v3.SourceGenerators.Shared.Constants;
using MusicClub.v3.SourceGenerators.Shared.Extensions;
using MusicClub.v3.SourceGenerators.Shared.Receivers;
using MusicClub.v3.SourceGenerators.Shared.Strings;
using System.Linq;
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
                if (!(attributeData.GetConstPropertyValue("ValidationPattern") is string validationPattern))
                {
                    continue;
                }

                var filterRequestTypeName = context.GetClassName(requestClassDeclarationSyntax);

                if (!Regex.IsMatch(filterRequestTypeName, validationPattern))
                {
                    continue;
                }

                if (!(attributeData.GetConstPropertyValue("ClassNameReplacePattern") is string classNameReplacePattern))
                {
                    continue;
                }
                if (!(attributeData.GetConstPropertyValue("ClassNameReplacement") is string classNameReplacement))
                {
                    continue;
                }

                var filterResponseTypeName = Regex.Replace(filterRequestTypeName, classNameReplacePattern, classNameReplacement);

                if (!(attributeData.GetConstPropertyValue("ClassNameSuffix") is string classNameSuffix))
                {
                    continue;
                }

                var filterRequestExtensionsClassName = filterRequestTypeName + classNameSuffix;
                var filterResponseExtensionsClassName = filterResponseTypeName + classNameSuffix;

                if (!(attributeData.GetConstPropertyValue("NamespaceReplacePattern") is string namespaceReplacePattern))
                {
                    continue;
                }
                if (!(attributeData.GetConstPropertyValue("NamespaceReplacement") is string namespaceReplacement))
                {
                    continue;
                }

                if (!new Regex(namespaceReplacePattern).TryReplace(context.GetNamespace(requestClassDeclarationSyntax), namespaceReplacement, out string baseNamespace))
                {
                    continue;
                }

                if (!(attributeData.GetConstPropertyValue("Request") is string request))
                {
                    continue;
                }
                if (!(attributeData.GetConstPropertyValue("Response") is string response))
                {
                    continue;
                }

                var filterRequestExtensionsNamespace = baseNamespace + "." + request;
                var filterResponseExtensionsNamespace = baseNamespace + "." + response;

                //todo => get the props through the attributeData, now is done through the attributeSyntax
                var interfaceProperties = context.GetInterfacePropertiesFromAttributeConstructorParam(requestClassDeclarationSyntax, "GenerateFilterMappers");
                var properties = context.GetPropertySymbols(requestClassDeclarationSyntax).Concat(interfaceProperties);

                if (!(attributeData.GetConstPropertyValue("ForeignKeyReplacePattern") is string foreignKeyReplacePattern))
                {
                    continue;
                }
                if (!(attributeData.GetConstPropertyValue("ForeignKeyReplacement") is string foreignKeyReplacement))
                {
                    continue;
                }

                var propertyNames = properties.Select(p => p.Name);
                var additionalPropertyNames = propertyNames.ReplaceMatches(foreignKeyReplacePattern, foreignKeyReplacement);

                context.AddSource(filterRequestExtensionsClassName + NamingConventions.FileExtension, ClassStrings.GetRequestToResponseString(filterRequestExtensionsNamespace, filterRequestExtensionsClassName, filterRequestTypeName, filterResponseTypeName, propertyNames, additionalPropertyNames));
                context.AddSource(filterResponseExtensionsClassName + NamingConventions.FileExtension, ClassStrings.GetResponseToRequestMapperString(filterResponseExtensionsNamespace, filterResponseExtensionsClassName, filterResponseTypeName, filterRequestTypeName, properties));
            }
        }
    }
}
