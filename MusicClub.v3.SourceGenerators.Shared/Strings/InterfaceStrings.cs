using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicClub.v3.SourceGenerators.Shared.Strings
{
    public static class InterfaceStrings
    {
        public static string GetIServiceString(string @namespace, string model, string baseInterfaceName, IEnumerable<string> baseInterfaceTypeParameterNames = null)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"namespace {@namespace}");
            stringBuilder.AppendLine($"{{");

            if (baseInterfaceTypeParameterNames is null || baseInterfaceTypeParameterNames.Count() == 0)
            {
                stringBuilder.AppendLine($"\tpublic interface I{model}Service : {baseInterfaceName} {{ }}");
            }
            else
            {
                stringBuilder.AppendLine($"\tpublic interface I{model}Service : {baseInterfaceName}<{string.Join(", ", baseInterfaceTypeParameterNames)}> {{ }}");
            }

            stringBuilder.AppendLine($"}}");

            return stringBuilder.ToString();
        }
    }
}
