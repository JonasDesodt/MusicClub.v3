using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace MusicClub.v3.SourceGenerators.Shared.Extensions
{
    public static class GenerationExecutionContextExtensions
    {
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
            foreach(var classDeclarationSyntax in classDeclarationSyntaxes)
            {
                if (context.Compilation.GetSemanticModel(classDeclarationSyntax.SyntaxTree).GetDeclaredSymbol(classDeclarationSyntax) is INamedTypeSymbol classSymbol)
                {
                    if (classSymbol.Name.EndsWith(suffix))
                    {
                        //if (classSymbol.ContainingNamespace != null && classSymbol.ContainingNamespace.IsGlobalNamespace)
                        //{
                            yield return classDeclarationSyntax;
                        //}
                    }
                }
            }
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
