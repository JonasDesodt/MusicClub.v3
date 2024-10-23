using Microsoft.CodeAnalysis;
using MusicClub.v3.SourceGenerators.Shared.Constants;
using MusicClub.v3.SourceGenerators.Shared.Extensions;
using MusicClub.v3.SourceGenerators.Shared.Helpers;
using MusicClub.v3.SourceGenerators.Shared.Receivers;
using MusicClub.v3.SourceGenerators.Shared.Strings;

namespace MusicClub.v3.SourceGenerators.Abstractions
{
    [Generator]
    internal class IServicesSourceGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new InterfaceDeclarationSyntaxReceiver());

        }
        public void Execute(GeneratorExecutionContext context)
        {
            if (!(context.SyntaxReceiver is InterfaceDeclarationSyntaxReceiver receiver))
            {
                return;
            }

            foreach (var (interfaceDeclarationSyntax, models) in receiver.GetModels(context.Compilation, Constants.GenerateIServicesAttributeName))
            {
                foreach (var model in models)
                {
                    var @namespace = context.GetNamespace(interfaceDeclarationSyntax);
                    var baseInterface = context.GetClassname(interfaceDeclarationSyntax);
                    var baseInterfceTypeParams = StringFormattingHelpers.ReplaceWithModelBeforeNamingConvention(model, context.GetTypeParameterNames(interfaceDeclarationSyntax), NamingConventions.GetDto());

                    context.AddSource($"I{model}Service{NamingConventions.FileExtension}", InterfaceStrings.GetIServiceString(@namespace, model, baseInterface, baseInterfceTypeParams));
                }
            }
        }


    }
}
