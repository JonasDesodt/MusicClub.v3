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

            foreach (var (classDeclarationSyntax, symbol, attributeData) in receiver.GetClassDeclarationSyntaxWithAttributeData(context, "GenerateDataResponse"))
            {
                var constants = attributeData.GetConstants(new string[]
                {
                    "ClassNamePattern",
                    "ClassNameReplacement",
                    "NamespacePattern",
                    "NamespaceReplacement",
                    "ForeignKeyPattern",
                    "ForeignKeyReplacement"
                });

                var classNamePattern = constants["ClassNamePattern"];

                var classNameReplacement = constants["ClassNameReplacement"];

                var namespacePattern = constants["NamespacePattern"];

                var namespaceReplacement = constants["NamespaceReplacement"];

                var foreignKeyPattern = constants["ForeignKeyPattern"];

                var foreignKeyReplacement = constants["ForeignKeyReplacement"];


                //todo => add check on request class name if a match is found with the classNamePattern?
                //[GenerateDataResponse] w/ wrong pattern on ArtistFilterRequest gives a results, should not happen

                var className = Regex.Replace(symbol.GetClassName(), classNamePattern, classNameReplacement);
                var @namespace = Regex.Replace(symbol.GetNamespace(), namespacePattern, namespaceReplacement);
               
                var properties = symbol.GetInterfaceProperties();

                context.AddSource(className + NamingConventions.FileExtension, ClassStrings.GetResponseString(@namespace, className, properties.GetDataResponsePropertyStrings(foreignKeyPattern, foreignKeyReplacement)));
            }
        }
    }
}
