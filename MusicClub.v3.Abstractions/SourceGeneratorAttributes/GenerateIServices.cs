using System.Diagnostics.CodeAnalysis;

namespace MusicClub.v3.Abstractions.SourceGeneratorAttributes
{
    [AttributeUsage(AttributeTargets.Interface)]
    internal class GenerateIServices(params string[] models) : Attribute
    {
        [SuppressMessage("Style", "IDE0052:Remove unread private member", Justification = "The constructor param is used by source generators")]
        private readonly string[] _models = models;
    }
}