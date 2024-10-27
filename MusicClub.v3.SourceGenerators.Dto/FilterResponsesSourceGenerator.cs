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
    internal class FilterResponsesSourceGenerator : ISourceGenerator
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

            foreach (var (classDeclarationSyntax, attributeData) in receiver.GetClassDeclarationSyntaxWithAttributeData(context.Compilation, "GenerateFilterResponse"))
            {
                if (!(attributeData.GetPropertyValue("ClassNamePattern") is string classNamePattern))
                {
                    continue;
                }
                if (!(attributeData.GetPropertyValue("ClassNameReplacement") is string classNameReplacement))
                {
                    continue;
                }

                if (!(attributeData.GetPropertyValue("NamespacePattern") is string namespacePattern))
                {
                    continue;
                }
                if (!(attributeData.GetPropertyValue("NamespaceReplacement") is string namespaceReplacement))
                {
                    continue;
                }

                if (!(attributeData.GetPropertyValue("ForeignKeyPattern") is string foreignKeyPattern))
                {
                    continue;
                }
                if (!(attributeData.GetPropertyValue("ForeignKeyReplacement") is string foreignKeyReplacement))
                {
                    continue;
                }


                //todo => add check on request class name if a match is found with the classNamePattern? also check on namespace ?
                //[GenerateDataResponse] w/ wrong pattern on ArtistFilterRequest gives a results, should not happen

                var className = Regex.Replace(context.GetClassName(classDeclarationSyntax), classNamePattern, classNameReplacement);
                var @namespace = Regex.Replace(context.GetNamespace(classDeclarationSyntax), namespacePattern, namespaceReplacement);
                
                //todo => get the props through the attributeData, now is done through the attributeSyntax
                var interfaceProperties = context.GetInterfacePropertiesFromAttributeConstructorParam(classDeclarationSyntax, "GenerateFilterResponse");
                var properties = context.GetPropertySymbols(classDeclarationSyntax).Concat(interfaceProperties);

                context.AddSource(className + NamingConventions.FileExtension, ClassStrings.GetResponseString(@namespace, className, properties.GetFilterResponsePropertyStrings(foreignKeyPattern, foreignKeyReplacement)));
            }
        }


    }
}
