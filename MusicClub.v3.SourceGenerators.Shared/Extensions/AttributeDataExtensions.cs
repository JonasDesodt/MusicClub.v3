using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace MusicClub.v3.SourceGenerators.Shared.Extensions
{
    internal static class AttributeDataExtensions
    {
        public static IEnumerable<string> GetParamStringArrayValues(this AttributeData attributeData)
        {
            foreach (var constructorArg in attributeData.ConstructorArguments)
            {
                if (constructorArg.Kind == TypedConstantKind.Array)
                {
                    foreach (var element in constructorArg.Values)
                    {
                        if (element.Type.SpecialType == SpecialType.System_String && element.Value is string stringValue)
                        {
                            yield return stringValue;
                        }
                    }
                }
            }
        }
    }
}
