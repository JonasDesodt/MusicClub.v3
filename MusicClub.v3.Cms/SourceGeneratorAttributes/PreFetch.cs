using System.Diagnostics.CodeAnalysis;

namespace MusicClub.v3.Cms.SourceGeneratorAttributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class PreFetch(string model) : Attribute
    {
        [SuppressMessage("Style", "IDE0052:Remove unread private member", Justification = "The constructor param is used by source generators")]
        private readonly string _model = model;
    }
}