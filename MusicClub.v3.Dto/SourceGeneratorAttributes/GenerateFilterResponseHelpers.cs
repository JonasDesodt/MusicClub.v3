using System.Diagnostics.CodeAnalysis;

namespace MusicClub.v3.Dto.SourceGeneratorAttributes
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]

    internal class GenerateFilterResponseHelpers(params string[] models) : Attribute
    {
        [SuppressMessage("Style", "IDE0052:Remove unread private member", Justification = "The constructor param is used by source generators")]
        private readonly string[] _models = models;
    }
}
