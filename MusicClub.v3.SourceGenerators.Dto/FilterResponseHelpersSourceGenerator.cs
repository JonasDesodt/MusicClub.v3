using Microsoft.CodeAnalysis;
using MusicClub.v3.SourceGenerators.Shared.Extensions;
using MusicClub.v3.SourceGenerators.Shared.Receivers;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MusicClub.v3.SourceGenerators.Dto
{
    [Generator]
    internal class FilterResponseHelpersSourceGenerator : ISourceGenerator
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

            foreach (var (interfaceDeclarationSyntax, models) in receiver.GetModels(context.Compilation, "GenerateFilterResponseHelpers"))
            {
                var methodSymbols = context.GetMethodSymbols(interfaceDeclarationSyntax);

                var interfaceSymbol = context.Compilation.GetSemanticModel(interfaceDeclarationSyntax.SyntaxTree).GetDeclaredSymbol(interfaceDeclarationSyntax) as INamedTypeSymbol;

                foreach (var model in models)
                {
                    var code = BuildFilterResponseHelperClass(context.GetNamespace(interfaceDeclarationSyntax), interfaceSymbol.Name.Replace("I", model), interfaceSymbol.Name, model, methodSymbols);

                    context.AddSource($"{interfaceSymbol.Name.Replace("I", model)}.g.cs", code);
                }

            }

        }

        private string BuildFilterResponseHelperClass(string containingNamespace, string className, string interfaceName, string model, IEnumerable<IMethodSymbol> methodSymbols)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"#nullable enable");
            builder.AppendLine();

            builder.AppendLine($"namespace {containingNamespace}");
            builder.AppendLine($"{{");

            builder.AppendLine($"\tpublic partial class {className} : {interfaceName}<{model}FilterRequest, {model}FilterResponse>");
            builder.AppendLine($"\t{{");

            foreach (var methodSymbol in methodSymbols)
            {
                var annotations = methodSymbol.GetAttributes();

                if (annotations.SingleOrDefault(x => x.AttributeClass?.Name == "ClassSuffix") is AttributeData classSuffixAttribute)
                {
                    //var suffix = classSuffixAttribute.NamedArguments.FirstOrDefault().Value;
                    var suffix = classSuffixAttribute.ConstructorArguments[0].Value.ToString();
                    if(suffix is null)
                    {
                        continue;
                    }

                    var interfaceMethodSignature = methodSymbol.GetInterfaceMethodSignature();

                    builder.AppendLine($"\t\tpublic {methodSymbol.GetInterfaceMethodSignature().Replace("TFilterRequest", model + "FilterRequest").Replace("TFilterResponse", model + "FilterResponse")}");
                    builder.AppendLine($"\t\t{{");

                    builder.AppendLine($"\t\t\treturn {model}{suffix}.{methodSymbol.Name}({string.Join(", ", methodSymbol.Parameters.Select(p => p.Name))});");

                    builder.AppendLine($"\t\t}}");
                }
            }


            builder.AppendLine($"\t}}");
            builder.AppendLine($"}}");

            return builder.ToString();
        }
    }
}
