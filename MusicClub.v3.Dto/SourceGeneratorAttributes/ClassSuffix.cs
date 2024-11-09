using System.Diagnostics.CodeAnalysis;

namespace MusicClub.v3.Dto.SourceGeneratorAttributes
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class ClassSuffix(string suffix) : Attribute
    {
        public string Suffix { get; } = suffix;
    }
}
