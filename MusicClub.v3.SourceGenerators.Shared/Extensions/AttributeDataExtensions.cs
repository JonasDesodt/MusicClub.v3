using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace MusicClub.v3.SourceGenerators.Shared.Extensions
{
    public static class AttributeDataExtensions
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

        public static string GetPropertyValue(this AttributeData attributeData, string name)
        {
            foreach (var namedArg in attributeData.AttributeClass.GetMembers().OfType<IFieldSymbol>())
            {
                if (namedArg.IsConst && namedArg.Type.SpecialType == SpecialType.System_String && namedArg.Name == name)
                {
                    return namedArg.ConstantValue as string;
                }
            }

            return null;
        }
    }
}
