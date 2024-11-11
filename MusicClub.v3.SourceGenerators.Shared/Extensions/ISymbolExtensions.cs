using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace MusicClub.v3.SourceGenerators.Shared.Extensions
{
    public static class ISymbolExtensions
    {
        public static string GetNamespace(this ISymbol symbol)
        {
            var namespaceSymbol = symbol.ContainingNamespace;

            if (namespaceSymbol.IsGlobalNamespace)
            {
                return string.Empty; // Return empty string if the class is in the global namespace
            }

            return namespaceSymbol.ToDisplayString();
        }

        public static string GetClassName(this ISymbol symbol)
        {
            return symbol.Name;
        }

        public static string GetInterfaceName(this ISymbol symbol)
        {
            return symbol.Name;
        }

        public static IEnumerable<IPropertySymbol> GetInterfaceProperties(this ISymbol symbol, string name = null)
        {
            foreach (var interfaceSymbol in (symbol as INamedTypeSymbol).AllInterfaces.Where(i => name == null || i.Name == name)) //todo => limit to the iModel? 
            {
                foreach (var member in interfaceSymbol.GetMembers().OfType<IPropertySymbol>())
                {
                    yield return member;
                }
            }
        }
    }
}
