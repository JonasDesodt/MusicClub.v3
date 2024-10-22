using System.Text;

namespace MusicClub.v3.SourceGenerators.Shared.Extensions
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendNullable(this StringBuilder stringBuilder) => stringBuilder.AppendLine($"#nullable enable");
    }
}
