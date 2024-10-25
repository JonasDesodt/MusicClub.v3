using System.Text.RegularExpressions;

namespace MusicClub.v3.SourceGenerators.Shared.Extensions
{
    public static class RegexExtensions
    {
        public static bool TryReplace(this Regex regex, string input, string replacement, out string result)
        {
            if (regex.IsMatch(input))
            {
                result = regex.Replace(input, replacement);

                return true;
            }

            result = input;

            return false;
        }
    }
}
