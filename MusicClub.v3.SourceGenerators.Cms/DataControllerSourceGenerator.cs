using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MusicClub.v3.SourceGenerators.Shared.Receivers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicClub.v3.SourceGenerators.Cms
{
    [Generator]
    internal class DataControllerSourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            if (!(context.SyntaxReceiver is ClassDeclarationSyntaxReceiver receiver))
                return;

            var parameters = new Dictionary<string, string>();

            var dataController = FindGeneratedDataControllerAttribute(receiver, context.Compilation);
            if (dataController == null)
            {
                return;
            }

            foreach (var (fieldName, model) in dataController.GetFieldsWithPreFetchAttribute())
            {
                parameters[fieldName] = model;
            }
            var className = dataController.Identifier.Text;
            var containingNamespace = NamespaceHelper.GetContainingNamespace(dataController);

            context.AddSource($"{className}.g.cs", GenerateClass(containingNamespace, className, parameters));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new ClassDeclarationSyntaxReceiver());
        }

        private string GenerateRouteHandlers(string name, string type)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"\t\t\tif(new Regex(@$\"^/{type.ToLower()}/edit/\\d+$\").IsMatch(route) || new Regex(@$\"^/{type.ToLower()}/delete/\\d+$\").IsMatch(route))");
            builder.AppendLine($"\t\t\t{{");
            builder.AppendLine($"\t\t\t\treturn await HandleDeleteEditRouteMatchFound({name}, route);");
            builder.AppendLine($"\t\t\t}}");

            builder.AppendLine($"\t\t\tif(new Regex(@$\"^/{type.ToLower()}$\").IsMatch(route))");
            builder.AppendLine($"\t\t\t{{");
            builder.AppendLine($"\t\t\t\treturn await HandleIndexRouteMatchFound({name}, new {type}FilterResponseHelpers());");
            builder.AppendLine($"\t\t\t}}");

            return builder.ToString();
        }

        private string GenerateClass(string containingNamespace, string className, Dictionary<string, string> parameters)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"#nullable enable");
            builder.AppendLine();
            builder.AppendLine($"using System.Text.RegularExpressions;");
            builder.AppendLine($"\nnamespace {containingNamespace}");
            builder.AppendLine($"{{");
            builder.AppendLine($"\tpublic partial class {className}");
            builder.AppendLine($"\t{{");
            builder.AppendLine($"\t\tprotected override partial async Task<bool> HandleRoute(string route)");
            builder.AppendLine($"\t\t{{");

            foreach (var type in parameters)
            {
                builder.Append(GenerateRouteHandlers(type.Key, type.Value));
            }
            builder.AppendLine($"\t\t\treturn await HandleCustomRoute(route);");
            builder.AppendLine($"\t\t}}");
            builder.AppendLine($"\t}}");
            builder.AppendLine($"}}");

            return builder.ToString();
        }

        private ClassDeclarationSyntax FindGeneratedDataControllerAttribute(ClassDeclarationSyntaxReceiver receiver, Compilation compilation)
        {
            foreach (var classDeclaration in receiver.Classes)
            {
                var semanticModel = compilation.GetSemanticModel(classDeclaration.SyntaxTree);
                var classSymbol = semanticModel.GetDeclaredSymbol(classDeclaration);
                if (classSymbol is null)
                {
                    continue;
                }

                foreach (var attributeData in classSymbol.GetAttributes())
                {

                    if (attributeData.AttributeClass?.Name == $"GenerateDataController")
                    {
                        return classDeclaration;
                    }
                }

            }

            return null;
        }
    }

    public static class ClassDeclarationExtensions
    {
        public static IEnumerable<(string FieldName, string ModelValue)> GetFieldsWithPreFetchAttribute(
         this ClassDeclarationSyntax classDeclaration)
        {
            // Find all fields in the class
            var fields = classDeclaration.Members.OfType<FieldDeclarationSyntax>();

            foreach (var field in fields)
            {
                // Iterate through each variable declared in the field (in case of multiple variable declarations in one field)
                foreach (var variable in field.Declaration.Variables)
                {
                    // Check if the field has the [PreFetch] attribute
                    var preFetchAttribute = field.AttributeLists
                        .SelectMany(attrList => attrList.Attributes)
                        .FirstOrDefault(attr => attr.Name.ToString() == "PreFetch");

                    // If it has the [PreFetch] attribute, extract the 'Model' property
                    if (preFetchAttribute != null)
                    {
                        // Try to get the named argument for the 'Model' property
                        var modelArgument = preFetchAttribute.ArgumentList?.Arguments
                            .FirstOrDefault()
                            ?.Expression;

                        // If the model argument is found, get its value
                        var modelValue = modelArgument is LiteralExpressionSyntax literalExpression
                            ? literalExpression.Token.ValueText  // Get the value if it's a string literal
                            : modelArgument?.ToString() ?? "unknown";  // Otherwise, get the expression as string


                        var fieldName = variable.Identifier.Text;

                        yield return (fieldName, modelValue);
                    }
                }
            }
        }
    }

    public static class NamespaceHelper
    {
        public static string GetContainingNamespace(ClassDeclarationSyntax classDeclaration)
        {
            // Traverse up the tree to find the nearest NamespaceDeclarationSyntax
            var namespaceDeclaration = classDeclaration.Ancestors()
                .OfType<BaseNamespaceDeclarationSyntax>() // This covers both NamespaceDeclarationSyntax and FileScopedNamespaceDeclarationSyntax
                .FirstOrDefault();

            if (namespaceDeclaration != null)
            {
                // Retrieve the namespace as a string
                return namespaceDeclaration.Name.ToString();
            }

            // If no namespace is found, it means the class is in the global namespace
            return "global";
        }
    }
}