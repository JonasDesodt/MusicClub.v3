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

            foreach (var (interfaceDeclarationSyntax, symbol, models) in receiver.GetModels(context, Constants.GenerateIServicesAttributeName))
            {
                foreach (var model in models)
                {
                    var @namespace = symbol.GetNamespace();
                    var baseInterface = symbol.GetInterfaceName();

                    var baseInterfceTypeParams = StringFormattingHelpers.ReplaceWithModelBeforeNamingConvention(model, context.GetTypeParameterNames(interfaceDeclarationSyntax), NamingConventions.GetDtoSuffixes());

                    context.AddSource($"I{model}Service{NamingConventions.FileExtension}", InterfaceStrings.GetIServiceString(@namespace, model, baseInterface, baseInterfceTypeParams));
                }
            }
        }


    }
}
