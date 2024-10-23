using System.Diagnostics.CodeAnalysis;

namespace MusicClub.v3.Api.SourceGeneratorAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class GenerateControllers(params string[] models) : Attribute 
    {
        [SuppressMessage("Style", "IDE0052:Remove unread private member", Justification = "The constructor param is used by source generators")]
        private readonly string[] _models = models;
    }
}
