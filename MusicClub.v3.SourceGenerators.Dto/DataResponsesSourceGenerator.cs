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

                var className = Regex.Replace(context.GetClassName(classDeclarationSyntax), classNamePattern, classNameReplacement);
                //var dataResponseClassName = Regex.Replace(context.GetClassName(classDeclarationSyntax), @"(?<=\S)DataRequest(?!.+DataRequest)", "DataResponse");
                var @namespace = Regex.Replace(context.GetNamespace(classDeclarationSyntax), namespacePattern, namespaceReplacement);

                context.AddSource(className + NamingConventions.FileExtension, ClassStrings.GetDataResponseString(@namespace, className));
            }

        }


    }
}
