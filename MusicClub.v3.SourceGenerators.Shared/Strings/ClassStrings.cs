using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MusicClub.v3.SourceGenerators.Shared.Constants;
using MusicClub.v3.SourceGenerators.Shared.Helpers;

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



    }
}
