﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MusicClub.v3.SourceGenerators.Shared.Extensions;

namespace MusicClub.v3.SourceGenerators.Shared.Strings
{
    public static class ClassStrings
    {
        public static string GetAttributeString(string @namespace, AttributeTargets attributeTargets, string name, string @params = null)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"namespace {@namespace}");
            stringBuilder.AppendLine($"{{");

            stringBuilder.AppendLine($"\t[AttributeUsage(AttributeTargets.{attributeTargets})]");

            if (string.IsNullOrWhiteSpace(@params))
            {
                stringBuilder.AppendLine($"\tinternal partial class {name} : Attribute {{ }}");
            }
            else
            {
                stringBuilder.AppendLine($"\tinternal partial class {name}(params string[] models) : Attribute {{ }}");
            }

            stringBuilder.AppendLine($"}}");

            return stringBuilder.ToString();
        }

        public static string GetControllerString(string @namespace, string model, IEnumerable<ParameterSyntax> constructorParameterSyntaxes, string baseClassName, IEnumerable<string> baseClassTypeParameterNames = null)
        {

            //todo => prepare the constructor params dynamically
            (string Type, string Name) dbService = ($"I{model}Service", constructorParameterSyntaxes.Single().Identifier.ToString());

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"namespace {@namespace}");
            stringBuilder.AppendLine($"{{");


            //todo: add constructor params dynamically, eg allow there to be none
            if (baseClassTypeParameterNames is null || baseClassTypeParameterNames.Count() == 0)
            {

                stringBuilder.AppendLine($"\tpublic class {model}Controller({dbService.Type} {dbService.Name}) : {baseClassName}({dbService.Name}) {{ }}");
            }
            else
            {
                stringBuilder.AppendLine($"\tpublic class {model}Controller({dbService.Type} {dbService.Name}) : {baseClassName}<{string.Join(", ", baseClassTypeParameterNames)}>({dbService.Name}) {{ }}");
            }

            stringBuilder.AppendLine($"}}");

            return stringBuilder.ToString();
        }

        public static string GetResponseString(string @namespace, string className, IEnumerable<string> propertyStrings)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendNullable();

            stringBuilder.AppendLine($"namespace {@namespace}");
            stringBuilder.AppendLine($"{{");

            stringBuilder.AppendLine($"\tpublic partial class {className}");
            stringBuilder.AppendLine($"\t{{");

            foreach (var propertyString in propertyStrings)
            {
                stringBuilder.AppendLine($"\t\t" + propertyString);
            }

            stringBuilder.AppendLine($"\t}}");
            stringBuilder.AppendLine($"}}");

            return stringBuilder.ToString();
        }

        public static string GetResponseToRequestMapperString(string @namespace, string responseExtensionsClassName, string responseTypeName, string requestTypeName, IEnumerable<IPropertySymbol> classProperties)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendNullable();

            stringBuilder.AppendLine($"namespace {@namespace}");
            stringBuilder.AppendLine($"{{");

            stringBuilder.AppendLine($"\tpublic static partial class {responseExtensionsClassName}");
            stringBuilder.AppendLine($"\t{{");

            stringBuilder.AppendLine($"\t\tpublic static {requestTypeName} ToRequest({responseTypeName} response)");
            stringBuilder.AppendLine($"\t\t{{");

            stringBuilder.AppendLine($"\t\t\treturn new {requestTypeName}");
            stringBuilder.AppendLine($"\t\t\t{{");

            foreach (var property in classProperties)
            {
                stringBuilder.AppendLine($"\t\t\t\t{property.Name} = response.{property.Name},");
            }

            stringBuilder.AppendLine($"\t\t\t}};");

            stringBuilder.AppendLine($"\t\t}}");

            stringBuilder.AppendLine($"\t}}");
            stringBuilder.AppendLine($"}}");

            return stringBuilder.ToString();
        }

        public static string GetRequestToResponseString(string @namespace, string requestExtensionsClassName, string requestTypeName, string responseTypeName, IEnumerable<string> properties, IEnumerable<string> additionalProperties)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendNullable();

            stringBuilder.AppendLine($"namespace {@namespace}");
            stringBuilder.AppendLine($"{{");

            stringBuilder.AppendLine($"\tpublic static partial class {requestExtensionsClassName}");
            stringBuilder.AppendLine($"\t{{");

            stringBuilder.Append($"\t\tpublic static {responseTypeName} ToResponse(this {requestTypeName} request");
            if(additionalProperties.Count() > 0)
            {
                stringBuilder.Append($", {string.Join(", ", additionalProperties.Select(x => x + "? " + x.ConvertFirstLetterToLowerCase()))}");
            }
            stringBuilder.Append($")");
            stringBuilder.AppendLine($"\n\t\t{{");
            stringBuilder.AppendLine($"\t\t\treturn new {responseTypeName}");
            stringBuilder.AppendLine($"\t\t\t{{");

            foreach (var property in properties)
            {
                stringBuilder.AppendLine($"\t\t\t\t{property} = request.{property},");
            }

            foreach(var property in additionalProperties)
            {
                stringBuilder.AppendLine($"\t\t\t\t{property} = {property.ConvertFirstLetterToLowerCase()},");
            }

            stringBuilder.AppendLine($"\t\t\t}};");
            stringBuilder.AppendLine($"\t\t}}");

            stringBuilder.AppendLine($"\t}}");

            stringBuilder.AppendLine($"}}");

            return stringBuilder.ToString();
        }
    }
}


