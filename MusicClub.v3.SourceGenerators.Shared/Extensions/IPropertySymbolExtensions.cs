using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MusicClub.v3.SourceGenerators.Shared.Extensions
{
    public static class IPropertySymbolExtensions
    {
        public static IEnumerable<string> GetDataResponsePropertyStrings(this IEnumerable<IPropertySymbol> propertySymbols, string foreignKeyPattern, string foreignKeyReplacement)
        {
            yield return $"public required int Id {{get; set; }}";

            yield return $"public required DateTime Created {{get; set; }}";
            yield return $"public required DateTime Updated {{get; set; }}";

            foreach (var property in propertySymbols)
            {
                if (Regex.Match(property.Name, foreignKeyPattern).Success)
                {
                    var name = Regex.Replace(property.Name, foreignKeyPattern, foreignKeyReplacement);

                    yield return $"public {(property.Type.NullableAnnotation != NullableAnnotation.Annotated || property.IsRequired ? "required " : string.Empty)}{name}{(property.Type.NullableAnnotation == NullableAnnotation.Annotated ? "?" : string.Empty)} {name} {{get; set; }}";
                }
                else
                {
                    yield return $"public {(property.Type.NullableAnnotation != NullableAnnotation.Annotated || property.IsRequired ? "required " : string.Empty)}{property.Type} {property.Name} {{get; set; }}";
                }
            }
        }

        public static IEnumerable<string> GetFilterResponsePropertyStrings(this IEnumerable<IPropertySymbol> propertySymbols, string foreignKeyPattern, string foreignKeyReplacement)
        {
            foreach (var property in propertySymbols)
            {
                if (Regex.Match(property.Name, foreignKeyPattern).Success)
                {
                    var dataResponseName = Regex.Replace(property.Name, foreignKeyPattern, foreignKeyReplacement);

                    yield return $"public {property.Type}{(property.Type.NullableAnnotation != NullableAnnotation.Annotated ? "?" : string.Empty)} {property.Name} {{get; set; }}";
                    yield return $"public {dataResponseName}? {dataResponseName} {{get; set; }}";
                }
                else
                {
                    yield return $"public {property.Type}{(property.Type.NullableAnnotation != NullableAnnotation.Annotated ? "?" : string.Empty)} {property.Name} {{get; set; }}";
                }
            }
        }
    }
}
