namespace MusicClub.v3.Dto.SourceGeneratorAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class GenerateFilterRequest(Type iModel) : Attribute
    {
        public const string ValidationPattern = @$".+{GeneratorConstants.Filter}{GeneratorConstants.Request}$";

    }
}
