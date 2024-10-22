namespace MusicClub.v3.SourceGenerators.Api
{
    internal static class Constants
    {
        public const string FileExtension = ".g.cs";


        public const string ProjectNamespace = "MusicClub.v3.Api";


        public const string AttributesNamespace = ProjectNamespace  + ".Attributes";

        public const string GenerateControllersAttributeName = "GenerateControllers";
        public const string GenerateControllersAttributeParams = "params string[] models";
        public const string GenerateControllersAttributeParamsTypes = "params string[]";


        public const string ApiControllersNamespace = ProjectNamespace + ".Controllers";
        public const string ApiControllerName = "ApiController";
    }
}
