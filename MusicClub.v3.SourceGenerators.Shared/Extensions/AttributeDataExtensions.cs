using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

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

        //public static IEnumerable<string> GetPropertyValues(this AttributeData attributeData, string name)
        //{
        //    // Find the named argument with the specified property name
        //    var namedArg = attributeData.NamedArguments
        //        .FirstOrDefault(arg => arg.Key == name);

        //    // Check if the named argument value is an array
        //    if (namedArg.Value.Kind == TypedConstantKind.Array)
        //    {
        //        // Extract and return the string values from the array
        //        return namedArg.Value.Values
        //            .Where(tc => tc.Kind == TypedConstantKind.Primitive && tc.Value is string)
        //            .Select(tc => (string)tc.Value);
        //    }

        //    // Return an empty array if the property is not found or is not an array of strings
        //    return Enumerable.Empty<string>();

        //    //var namedArg = attributeData.AttributeClass.GetMembers().OfType<IPropertySymbol>().FirstOrDefault(p => p.Name == name);
        //    // if (namedArg.Type is IArrayTypeSymbol arrayType)
        //    // {
        //    //     return new string[] { };
        //    // }

        //    // return new string[] { };
        //}
    }
}
