using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MusicClub.v3.SourceGenerators.Shared.Helpers
{
    public static class StringFormattingHelpers
    {
        public static IEnumerable<string> ReplaceWithModelBeforeNamingConvention(string model, IEnumerable<string> inputs, params string[] namingConventions)
        {
            foreach (var typeParameterName in inputs)
            {
                string pattern = $@"^.*(?={string.Join("|", namingConventions)})";

                yield return Regex.Replace(typeParameterName, pattern, model);
            }
        }
    }
}