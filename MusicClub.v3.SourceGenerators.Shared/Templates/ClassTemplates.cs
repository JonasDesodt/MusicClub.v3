using System;
using System.Text;

namespace MusicClub.v3.SourceGenerators.Shared.Templates
{
    public static class ClassTemplates
    {
        public static string GetAttributeTemplate(string @namespace, AttributeTargets attributeTargets, string name, string @params = null)
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
    }
}
