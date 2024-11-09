using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MusicClub.v3.SourceGenerators.Shared.Constants;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public static IEnumerable<IPropertySymbol> GetPropertySymbols(this GeneratorExecutionContext context, ClassDeclarationSyntax classDeclarationSyntax)
        {
            return context.GetNamedTypeSymbol(classDeclarationSyntax).GetMembers().OfType<IPropertySymbol>();
        }
        public static IEnumerable<string> GetPropertySymbolNames(this GeneratorExecutionContext context, ClassDeclarationSyntax classDeclarationSyntax)
        {
            return context.GetPropertySymbols(classDeclarationSyntax).Select(symbol => symbol.Name);
        }
        //public static IEnumerable<string> ReplacePropertySymbolNames(this GeneratorExecutionContext context, ClassDeclarationSyntax classDeclarationSyntax, IDictionary<string, string> replacementPatterns)
        //{
        //    foreach (var propertySymbol in context.GetPropertySymbols(classDeclarationSyntax))
        //    {
        //        foreach (var replacementPattern in replacementPatterns)
        //        {
        //            var regex = new Regex(replacementPattern.Value);
        //            if (regex.TryReplace(propertySymbol.Name, replacementPattern.Key, out string name))
        //            {
        //                yield return name;
        //                break;
        //            }
        //        }
        //    }
        //}


        //var classProperties = classSymbol.GetMembers().OfType<IPropertySymbol>();
        public static IEnumerable<ClassDeclarationSyntax> FilterClassesInGlobalNamespaceOnSuffix(this GeneratorExecutionContext context, IEnumerable<ClassDeclarationSyntax> classDeclarationSyntaxes, string suffix)
        {
            foreach (var classDeclarationSyntax in classDeclarationSyntaxes)
            {
                if (context.Compilation.GetSemanticModel(classDeclarationSyntax.SyntaxTree).GetDeclaredSymbol(classDeclarationSyntax) is INamedTypeSymbol classSymbol)
                {
                    if (classSymbol.Name.EndsWith(suffix))
                    {
                        if (/*classSymbol.ContainingNamespace != null && classSymbol.ContainingNamespace.Name*/ context.GetNamespace(classDeclarationSyntax) == context.GetRootNamespace()/*&& classSymbol.ContainingNamespace.IsGlobalNamespace*/)
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

        public static IEnumerable<IPropertySymbol> GetInterfaceProperties(this GeneratorExecutionContext context, ClassDeclarationSyntax classDeclaration, string name = null)
        {
            // Get the symbol representing the class
            var namedTypeSymbol = context.GetNamedTypeSymbol(classDeclaration);
            // Enumerate through all implemented interfaces            

            foreach (var interfaceSymbol in namedTypeSymbol.AllInterfaces.Where(i => name == null || i.Name == name)) //todo => limit to the iModel? 
            {
                // Get all members of the interface and filter to properties
                foreach (var member in interfaceSymbol.GetMembers().OfType<IPropertySymbol>())
                {
                    yield return member;
                }
            }
        }

        public static IEnumerable<IPropertySymbol> GetInterfacePropertiesFromAttributeConstructorParam(this GeneratorExecutionContext context, ClassDeclarationSyntax classDeclarationSyntax, string attributeName)
        {
            var model = context.Compilation.GetSemanticModel(classDeclarationSyntax.SyntaxTree);
            var attribute = classDeclarationSyntax.AttributeLists
                .SelectMany(attrList => attrList.Attributes)
                .FirstOrDefault(attr => attr.Name.ToString().Contains(attributeName));

            var attributeArgument = attribute.ArgumentList?.Arguments.FirstOrDefault();
            if (attributeArgument?.Expression is TypeOfExpressionSyntax typeOfExpression)
            {
                var typeSymbol = model.GetTypeInfo(typeOfExpression.Type).Type as INamedTypeSymbol;
                if (typeSymbol != null && typeSymbol.TypeKind == TypeKind.Interface)
                {
                    var properties = typeSymbol.GetMembers().OfType<IPropertySymbol>();
                    foreach (var property in properties)
                    {
                        yield return property;
                    }
                }
            }
        }

        public static IEnumerable<IMethodSymbol> GetMethodSymbols(this GeneratorExecutionContext context, InterfaceDeclarationSyntax interfaceDeclarationSyntax)
        {
            var semanticModel = context.Compilation.GetSemanticModel(interfaceDeclarationSyntax.SyntaxTree);

            foreach (var member in interfaceDeclarationSyntax.Members)
            {
                // Get the symbol for each member
                var memberSymbol = semanticModel.GetDeclaredSymbol(member);

                if (memberSymbol is IMethodSymbol methodSymbol)
                {
                    yield return methodSymbol;
                }
            }
        }

        public static IEnumerable<(IMethodSymbol, IList<AttributeData>)> GetMethodSymbolsAndTheirAnnotations(this GeneratorExecutionContext context, InterfaceDeclarationSyntax interfaceDeclarationSyntax)
        {
            var compilation = context.Compilation;

            var semanticModel = compilation.GetSemanticModel(interfaceDeclarationSyntax.SyntaxTree);

            foreach (var member in interfaceDeclarationSyntax.Members)
            {
                // Get the symbol for each member
                var memberSymbol = semanticModel.GetDeclaredSymbol(member);
                
                if (memberSymbol is IMethodSymbol methodSymbol)
                {
                    yield return (methodSymbol, methodSymbol.GetAttributes());

                }
            }
        }
    }
}
