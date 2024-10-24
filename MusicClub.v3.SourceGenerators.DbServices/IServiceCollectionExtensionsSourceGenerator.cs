using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MusicClub.v3.SourceGenerators.Shared.Constants;
using MusicClub.v3.SourceGenerators.Shared.Extensions;
using MusicClub.v3.SourceGenerators.Shared.Receivers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicClub.v3.SourceGenerators.DbServices
{
    [Generator]
    internal class IServiceCollectionExtensionsSourceGenerator : ISourceGenerator
    {
        private const string Classname = "IServiceCollectionExtensions";

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

            var options = context.AnalyzerConfigOptions.GlobalOptions;
            if (!options.TryGetValue("build_property.GenerateISourceCollectionExtensions", out var GenerateISourceCollectionExtensions) || !(bool.Parse(GenerateISourceCollectionExtensions) is true))
            {
                return;
            }


            //also check if the correct interface is applied (& implemented)
            var dbServices = context.FilterClassesInGlobalNamespaceOnSuffix(receiver.Classes, NamingConventions.DbServiceSuffix);

            var rootNamespace = context.GetRootNamespace();
            if (rootNamespace is null)
            {
                return;
            }

            context.AddSource(Classname + NamingConventions.FileExtension, GetIServiceCollectionExtensionsString(rootNamespace + ".Extensions", dbServices, context));
        }

        private string GetIServiceCollectionExtensionsString(string @namespace, IEnumerable<ClassDeclarationSyntax> dbServices, GeneratorExecutionContext context)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"namespace {@namespace}");
            stringBuilder.AppendLine($"{{");

            stringBuilder.AppendLine($"\tpublic static partial class {Classname}");
            stringBuilder.AppendLine($"\t{{");

            stringBuilder.AppendLine($"\t\tpublic static {FrameworkTypes.IServiceCollection} AddDbServices(this {FrameworkTypes.IServiceCollection} services)");
            stringBuilder.AppendLine($"\t\t{{");
            foreach (var dbService in dbServices)
            {
                var name = context.GetClassName(dbService); //todo => do this in execute method

                //crash if there is more than one I{Model}Service found
                var @interface = context.GetInterfacesWithPattern(dbService, NamingConventions.IModelServicePattern).Single().Name; //todo => do this in execute method

                stringBuilder.AppendLine($"\t\t\tservices.AddScoped<{@interface}, {name}>();");
            }
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"\t\t\treturn services;");
            stringBuilder.AppendLine($"\t\t}}");

            stringBuilder.AppendLine($"\t}}");

            stringBuilder.AppendLine($"}}");

            return stringBuilder.ToString();
        }


    }
}
