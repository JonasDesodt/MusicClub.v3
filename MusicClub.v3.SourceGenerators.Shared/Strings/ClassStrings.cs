using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public static string GetControllerString(string @namespace, string model, string baseClassName, IEnumerable<string> baseClassTypeParameterNames = null)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"namespace {@namespace}");
            stringBuilder.AppendLine($"{{");

            if (baseClassTypeParameterNames is null || baseClassTypeParameterNames.Count() == 0)
            {
                stringBuilder.AppendLine($"\tpublic class {model}Controller : {baseClassName} {{ }}");
            }
            else
            {
                stringBuilder.AppendLine($"\tpublic class {model}Controller : {baseClassName}<{string.Join(", ", baseClassTypeParameterNames)}> {{ }}");
            }

            stringBuilder.AppendLine($"}}");

            return stringBuilder.ToString();
        }



    }
}
