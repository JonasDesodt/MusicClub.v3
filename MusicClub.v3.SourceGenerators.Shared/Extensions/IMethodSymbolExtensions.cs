using Microsoft.CodeAnalysis;
using System.Linq;

namespace MusicClub.v3.SourceGenerators.Shared.Extensions
{
    public static class IMethodSymbolExtensions
    {
        public static string GetInterfaceMethodSignature(this IMethodSymbol methodSymbol)
        {
            var returnType = methodSymbol.ReturnType.ToDisplayString();

            // Get the method name
            var methodName = methodSymbol.Name;

            // Get the parameters
            var parameters = string.Join(", ", methodSymbol.Parameters.Select(p =>
            {
                var parameterType = p.Type.ToDisplayString();
                var parameterName = p.Name;
                return $"{parameterType} {parameterName}";
            }));

            // Build the full method signature
            return $"{returnType} {methodName}({parameters})";
        }
    }
}
