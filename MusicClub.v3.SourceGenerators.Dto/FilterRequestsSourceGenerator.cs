using Microsoft.CodeAnalysis;
using MusicClub.v3.SourceGenerators.Shared.Constants;
using MusicClub.v3.SourceGenerators.Shared.Extensions;
using MusicClub.v3.SourceGenerators.Shared.Receivers;
using MusicClub.v3.SourceGenerators.Shared.Strings;
using System;
using System.Text.RegularExpressions;

namespace MusicClub.v3.SourceGenerators.Dto
{
    [Generator]
    internal class FilterRequestsSourceGenerator : ISourceGenerator
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

            foreach (var (classDeclarationSyntax, attributeData) in receiver.GetClassDeclarationSyntaxWithAttributeData(context.Compilation, "GenerateFilterRequest"))
            {
                //VALIDATE THE CLASS
                if (!(attributeData.GetPropertyValue("ValidationPattern") is string validationPattern))
                {
                    continue;
                }

                var @class = context.GetClassName(classDeclarationSyntax);

                if (!Regex.IsMatch(@class, validationPattern))
                {
                    continue;
                }
                
                //todo => get the props through the attributeData, now is done through the attributeSyntax
                var properties = context.GetInterfacePropertiesFromAttributeConstructorParam(classDeclarationSyntax, "GenerateFilterRequest");

                var @namespace = context.GetNamespace(classDeclarationSyntax);

                context.AddSource(@class + NamingConventions.FileExtension, ClassStrings.GetIModelFilterRequestImplementationString(@namespace, @class, properties));
            }
        }
    }
}
