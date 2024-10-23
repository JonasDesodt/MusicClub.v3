using Microsoft.CodeAnalysis;
using MusicClub.v3.SourceGenerators.Shared.Constants;
using MusicClub.v3.SourceGenerators.Shared.Extensions;
using MusicClub.v3.SourceGenerators.Shared.Helpers;
using MusicClub.v3.SourceGenerators.Shared.Receivers;
using MusicClub.v3.SourceGenerators.Shared.Strings;

namespace MusicClub.v3.SourceGenerators.Api
{
    [Generator]
    public class ControllersSourceGenerator : ISourceGenerator
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

            foreach (var (classDeclarationSyntax, models) in receiver.GetModels(context.Compilation, Constants.GenerateControllersAttributeName))
            {
                foreach (var model in models)
                {
                    var @namespace = context.GetNamespace(classDeclarationSyntax);
                    var baseClass = context.GetClassname(classDeclarationSyntax);
                    var baseClassTypeParams = StringFormattingHelpers.ReplaceWithModelBeforeNamingConvention(model, context.GetTypeParameterNames(classDeclarationSyntax), NamingConventions.GetDto());
                    
                    context.AddSource($"{model}Controller{NamingConventions.FileExtension}", ClassStrings.GetControllerString(@namespace, model, baseClass, baseClassTypeParams));
                }
            }
        }
    }
}