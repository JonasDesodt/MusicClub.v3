using Microsoft.CodeAnalysis;

namespace MusicClub.v3.SourceGenerators.Shared.Extensions
{
    public static class INamedTypeSymbolExtensions
    {
        public static string GetContainingNamespace(this INamedTypeSymbol namedTypeSymbol)
        {
            var namespaceSymbol = namedTypeSymbol.ContainingNamespace;
            if (namespaceSymbol.IsGlobalNamespace)
            {
                return string.Empty; // Return empty string if the class is in the global namespace
            }

            // Construct the full namespace by walking the namespace hierarchy
            return namespaceSymbol.ToDisplayString();
        }
    }
}
