using Microsoft.CodeAnalysis;

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
    }
}
