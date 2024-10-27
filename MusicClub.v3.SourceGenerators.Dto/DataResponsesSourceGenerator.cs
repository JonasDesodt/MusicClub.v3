using Microsoft.CodeAnalysis;
using MusicClub.v3.SourceGenerators.Shared.Receivers;
using MusicClub.v3.SourceGenerators.Shared.Extensions;
using System.Text.RegularExpressions;
using MusicClub.v3.SourceGenerators.Shared.Constants;
using MusicClub.v3.SourceGenerators.Shared.Strings;

namespace MusicClub.v3.SourceGenerators.Dto
{
    [Generator]
    internal class DataResponsesSourceGenerator : ISourceGenerator
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

            foreach (var (classDeclarationSyntax, attributeData) in receiver.GetClassDeclarationSyntaxWithAttributeData(context.Compilation, "GenerateDataResponse"))
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

                //todo => add check on request class name if a match is found with the classNamePattern?
                //[GenerateDataResponse] w/ wrong pattern on ArtistFilterRequest gives a results, should not happen

                var className = Regex.Replace(context.GetClassName(classDeclarationSyntax), classNamePattern, classNameReplacement);
                var @namespace = Regex.Replace(context.GetNamespace(classDeclarationSyntax), namespacePattern, namespaceReplacement);
                //var properties = context.GetPropertySymbols(classDeclarationSyntax);
                var properties = context.GetInterfaceProperties(classDeclarationSyntax);

                context.AddSource(className + NamingConventions.FileExtension, ClassStrings.GetResponseString(@namespace, className, properties.GetDataResponsePropertyStrings(foreignKeyPattern, foreignKeyReplacement)));
            }
        }
    }
}
