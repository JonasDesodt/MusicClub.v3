using Microsoft.CodeAnalysis;
using MusicClub.v3.SourceGenerators.Shared.Constants;
using MusicClub.v3.SourceGenerators.Shared.Extensions;
using MusicClub.v3.SourceGenerators.Shared.Helpers;
using MusicClub.v3.SourceGenerators.Shared.Receivers;
using MusicClub.v3.SourceGenerators.Shared.Strings;

namespace MusicClub.v3.SourceGenerators.Api
{
    [Generator]
    internal class ControllersSourceGenerator : ISourceGenerator
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

            foreach (var (classDeclarationSyntax, symbol, models) in receiver.GetModels(context, Constants.GenerateControllersAttributeName))
            {
                foreach (var model in models)
                {
                    var @namespace = symbol.GetNamespace();
                    var baseClass = symbol.GetClassName();
                    var constructorParams = classDeclarationSyntax.GetSingleConstructorParameters();
                    var baseClassTypeParams = StringFormattingHelpers.ReplaceWithModelBeforeNamingConvention(model, context.GetTypeParameterNames(classDeclarationSyntax), NamingConventions.GetDtoSuffixes());

                    context.AddSource($"{model}Controller{NamingConventions.FileExtension}", ClassStrings.GetControllerString(@namespace, model, constructorParams, baseClass, baseClassTypeParams));
                }
            }
        }
    }
}