using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MusicClub.v3.SourceGenerators.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string ConvertFirstLetterToLowerCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            if(input.Length == 1)
            {
                return input.ToLower();
            }

            return char.ToLower(input[0]) + input.Substring(1);
        }

        public static IEnumerable<string> ReplaceMatches(this IEnumerable<string> input, string pattern, string replacement)
        {
            foreach(var item in input)
            {
                if(new Regex(pattern).TryReplace(item, replacement, out string result))
                {
                    yield return result;
                }
            }
        }
    }
}
