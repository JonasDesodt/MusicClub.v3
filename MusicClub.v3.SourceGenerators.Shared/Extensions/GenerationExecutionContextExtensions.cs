using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MusicClub.v3.SourceGenerators.Shared.Constants;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MusicClub.v3.SourceGenerators.Shared.Extensions
{
    public static class GenerationExecutionContextExtensions
    {
        public static string GetRootNamespace(this GeneratorExecutionContext context)
        {
            context.AnalyzerConfigOptions.GlobalOptions.TryGetValue(GlobalOptions.RootNamespace, out var rootNamespace);

            return rootNamespace;
        }

        private static string GetNamespace(this GeneratorExecutionContext context, SyntaxNode syntaxNode)
        {
            var semanticModel = context.Compilation.GetSemanticModel(syntaxNode.SyntaxTree);
            var namedTypeSymbol = semanticModel.GetDeclaredSymbol(syntaxNode) as INamedTypeSymbol;

            var namespaceSymbol = namedTypeSymbol.ContainingNamespace;
            if (namespaceSymbol.IsGlobalNamespace)
            {
                return string.Empty; // Return empty string if the class is in the global namespace
            }

            return namespaceSymbol.ToDisplayString();
        }

        private static string GetSyntaxNodeName(this GeneratorExecutionContext context, SyntaxNode syntaxNode)
        {
            var semanticModel = context.Compilation.GetSemanticModel(syntaxNode.SyntaxTree);
            var symbol = semanticModel.GetDeclaredSymbol(syntaxNode);

            return symbol.Name;
        }


        public static string GetNamespace(this GeneratorExecutionContext context, ClassDeclarationSyntax classDeclarationSyntax)
        {
            return GetNamespace(context, classDeclarationSyntax as SyntaxNode);
        }

        public static string GetClassName(this GeneratorExecutionContext context, ClassDeclarationSyntax classDeclarationSyntax)
        {
            return GetSyntaxNodeName(context, classDeclarationSyntax as SyntaxNode);
        }

        public static IEnumerable<string> GetTypeParameterNames(this GeneratorExecutionContext context, ClassDeclarationSyntax classDeclarationSyntax)
        {
            if (classDeclarationSyntax?.TypeParameterList != null)
            {
                foreach (var typeParameterSyntax in classDeclarationSyntax.TypeParameterList.Parameters)
                {
                    yield return context.Compilation.GetSemanticModel(typeParameterSyntax.SyntaxTree).GetDeclaredSymbol(typeParameterSyntax).Name;
                }
            }
        }

        public static IEnumerable<ClassDeclarationSyntax> FilterClassesInGlobalNamespaceOnSuffix(this GeneratorExecutionContext context, IEnumerable<ClassDeclarationSyntax> classDeclarationSyntaxes, string suffix)
        {
            foreach (var classDeclarationSyntax in classDeclarationSyntaxes)
            {
                if (context.Compilation.GetSemanticModel(classDeclarationSyntax.SyntaxTree).GetDeclaredSymbol(classDeclarationSyntax) is INamedTypeSymbol classSymbol)
                {
                    if (classSymbol.Name.EndsWith(suffix))
                    {
                        if (/*classSymbol.ContainingNamespace != null && classSymbol.ContainingNamespace.Name*/ context.GetNamespace(classDeclarationSyntax)== context.GetRootNamespace()/*&& classSymbol.ContainingNamespace.IsGlobalNamespace*/)
                        {
                            yield return classDeclarationSyntax;
                        }
                    }
                }
            }
        }

        public static IEnumerable<INamedTypeSymbol> GetInterfacesWithPattern(this GeneratorExecutionContext context, ClassDeclarationSyntax classDeclarationSyntax, Regex pattern)
        {
            foreach (var namedTypeSymbol in context.GetNamedTypeSymbol(classDeclarationSyntax).AllInterfaces)
            {
                if (pattern.IsMatch(namedTypeSymbol.Name)) 
                {
                    yield return namedTypeSymbol;
                }
            };
        }

        private static INamedTypeSymbol GetNamedTypeSymbol(this GeneratorExecutionContext context, ClassDeclarationSyntax classDeclarationSyntax)
        {
            var semanticModel = context.Compilation.GetSemanticModel(classDeclarationSyntax.SyntaxTree);
            return semanticModel.GetDeclaredSymbol(classDeclarationSyntax) as INamedTypeSymbol;

        }


        public static string GetNamespace(this GeneratorExecutionContext context, InterfaceDeclarationSyntax interfaceDeclarationSyntax)
        {
            return GetNamespace(context, interfaceDeclarationSyntax as SyntaxNode);
        }

        public static string GetInterfaceName(this GeneratorExecutionContext context, InterfaceDeclarationSyntax interfaceDeclarationSyntax)
        {
            return GetSyntaxNodeName(context, interfaceDeclarationSyntax as SyntaxNode);
        }

        public static IEnumerable<string> GetTypeParameterNames(this GeneratorExecutionContext context, InterfaceDeclarationSyntax interfaceDeclarationSyntax)
        {
            if (interfaceDeclarationSyntax?.TypeParameterList != null)
            {
                foreach (var typeParameterSyntax in interfaceDeclarationSyntax.TypeParameterList.Parameters)
                {
                    yield return context.Compilation.GetSemanticModel(typeParameterSyntax.SyntaxTree).GetDeclaredSymbol(typeParameterSyntax).Name;
                }
            }
        }
    }
}
