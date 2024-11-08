using Microsoft.CodeAnalysis;
using MusicClub.v3.SourceGenerators.Shared.Extensions;
using MusicClub.v3.SourceGenerators.Shared.Receivers;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace MusicClub.v3.SourceGenerators.Dto
{
    [Generator]
    internal class FilterRequestHelpersSourceGenerator : ISourceGenerator
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

            var annotatedInterfaces = receiver.GetModels(context.Compilation, "GenerateFilterRequestHelpers");

            foreach (var (interfaceDeclarationSyntax, models) in annotatedInterfaces)
            {
                var interfaceSymbol = context.Compilation.GetSemanticModel(interfaceDeclarationSyntax.SyntaxTree).GetDeclaredSymbol(interfaceDeclarationSyntax) as INamedTypeSymbol;

                foreach (var model in models)
                {
                    var code = BuildFilterRequestHelpersClass(context.GetNamespace(interfaceDeclarationSyntax), model, interfaceSymbol.Name.Replace("I", model), interfaceSymbol.Name, interfaceSymbol.GetMembers());

                    context.AddSource($"{interfaceSymbol.Name.Replace("I", model)}.g.cs", code);
                }
            }
        }

        private string BuildFilterRequestHelpersClass(string containingNamespace, string model, string className, string interfaceName, ImmutableArray<ISymbol> members)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"#nullable enable");
            builder.AppendLine();

            builder.AppendLine($"namespace {containingNamespace}");
            builder.AppendLine($"{{");
            builder.AppendLine($"\tpublic partial class {className} : {interfaceName}<{model}FilterRequest, {model}FilterResponse>");
            builder.AppendLine($"\t{{");

            foreach (var member in members)
            {
                if (member is IMethodSymbol methodSymbol)
                {
                    var interfaceMethodSignature = methodSymbol.GetInterfaceMethodSignature();

                    builder.AppendLine($"\t\tpublic {methodSymbol.GetInterfaceMethodSignature().Replace("TFilterRequest", model + "FilterRequest").Replace("TFilterResponse", model + "FilterResponse")}");
                    builder.AppendLine($"\t\t{{");

                    builder.AppendLine($"\t\t\treturn {model}FilterRequestExtensions.{methodSymbol.Name}({string.Join(", ", methodSymbol.Parameters.Select(p => p.Name))});");

                    builder.AppendLine($"\t\t}}");
                }
            }

            builder.AppendLine($"\t}}");
            builder.AppendLine($"}}");

            return builder.ToString();
        }


    }
}
