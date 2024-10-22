using Microsoft.CodeAnalysis;
using MusicClub.v3.SourceGenerators.Shared.Templates;
using System;

namespace MusicClub.v3.SourceGenerators.Api
{
    [Generator]
    public class AttributesSourceGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            return;
        }

        public void Execute(GeneratorExecutionContext context)
        {
            context.AddSource($"{Constants.GenerateControllersAttributeName}{Constants.FileExtension}", ClassTemplates.GetAttributeTemplate(Constants.AttributesNamespace, AttributeTargets.Class, Constants.GenerateControllersAttributeName, Constants.GenerateControllersAttributeParams));
        }
    }
}
