using Microsoft.CodeAnalysis;
using MusicClub.v3.SourceGenerators.Shared.Extensions;
using MusicClub.v3.SourceGenerators.Shared.Receivers;
using System.Text;

namespace MusicClub.SourceGenerators.ApiServices
{
    [Generator]
    public class ApiServicesSourceGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new ClassDeclarationSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (!(context.SyntaxReceiver is ClassDeclarationSyntaxReceiver receiver))
            {
                return;
            }

            foreach (var (classDeclaration, models) in receiver.GetModels(context.Compilation, "GenerateApiServices"))
            {
                foreach (var model in models)
                {
                    context.AddSource($"{model}ApiService.g.cs", GetApiServiceClass(context.GetNamespace(classDeclaration), model, classDeclaration.Identifier.Text));
                }
            }
        }   

        private string GetApiServiceClass(string containingNamespace, string model, string baseClassName)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"#nullable enable");
            builder.AppendLine();

            //todo: get APIService params dynamically 

            builder.AppendLine($"namespace {containingNamespace}");
            builder.AppendLine($"{{");
            builder.AppendLine($"\tpublic class {model}ApiService(IHttpClientFactory httpClientFactory, IFilterRequestHelpers<{model}FilterRequest, {model}FilterResponse> filterRequestHelpers) : {baseClassName}<{model}DataRequest,{model}DataResponse, {model}FilterRequest, {model}FilterResponse>(httpClientFactory, filterRequestHelpers), I{model}Service"); // todo: make the constructor params dynamic
            builder.AppendLine($"\t{{");
            builder.AppendLine($"\t\tprotected override string Endpoint {{ get; }} = \"{model}\";");
            builder.AppendLine($"\t}}");
            builder.AppendLine($"}}");

            return builder.ToString();
        }
    }
}
