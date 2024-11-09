using Microsoft.CodeAnalysis;
using MusicClub.v3.SourceGenerators.Shared.Constants;
using MusicClub.v3.SourceGenerators.Shared.Extensions;
using MusicClub.v3.SourceGenerators.Shared.Receivers;
using MusicClub.v3.SourceGenerators.Shared.Strings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MusicClub.v3.SourceGenerators.Dto
{
    [Generator]
    internal class IModelImplementationsSourceGenerator : ISourceGenerator
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

            foreach (var (requestClassDeclarationSyntax, attributeData) in receiver.GetClassDeclarationSyntaxWithAttributeData(context.Compilation, "GenerateIModelImplementation"))
            {
                //VALIDATE THE CLASS
                if (!(attributeData.GetConstPropertyValue("ValidationPattern") is string validationPattern))
                {
                    continue;
                }

                var @class = context.GetClassName(requestClassDeclarationSyntax);

                if (!Regex.IsMatch(@class, validationPattern))
                {
                    continue;
                }

                var properties = context.GetInterfaceProperties(requestClassDeclarationSyntax);

                var @namespace = context.GetNamespace(requestClassDeclarationSyntax);

                context.AddSource(@class + NamingConventions.FileExtension, ClassStrings.GetIModelImplementationString(@namespace, @class, properties));
            }
        }
    }
}
