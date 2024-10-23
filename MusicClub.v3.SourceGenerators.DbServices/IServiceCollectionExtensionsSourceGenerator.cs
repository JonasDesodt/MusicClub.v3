using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MusicClub.v3.SourceGenerators.Shared.Constants;
using MusicClub.v3.SourceGenerators.Shared.Extensions;
using MusicClub.v3.SourceGenerators.Shared.Receivers;
using System.Collections.Generic;
using System.Text;

namespace MusicClub.v3.SourceGenerators.DbServices
{
    [Generator]
    internal class IServiceCollectionExtensionsSourceGenerator : ISourceGenerator
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

            var dbServices = context.FilterClassesInGlobalNamespaceOnSuffix(receiver.Classes, NamingConventions.DbServiceSuffix);

            //todo => get the namespace dynamically
            context.AddSource("IServiceCollectionExtensions" + NamingConventions.FileExtension, GetIServiceCollectionExtensionsString("MusicClub.v3.DbServices.Extensions", dbServices, context));
        }

        private string GetIServiceCollectionExtensionsString(string @namespace, IEnumerable<ClassDeclarationSyntax> dbServices, GeneratorExecutionContext context)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"namespace {@namespace}");
            stringBuilder.AppendLine($"{{");

            stringBuilder.AppendLine($"\tpublic static partial class IServiceCollectionExtensions");
            stringBuilder.AppendLine($"\t{{");

            stringBuilder.AppendLine($"\t\tpublic static IServiceCollection AddDbServices(this IServiceCollection services)");
            stringBuilder.AppendLine($"\t\t{{");
            foreach (var dbService in dbServices)
            {
                var name = context.GetClassName(dbService); //todo => do this in execute method
                //todo => get the interface thats the class implements
                var @interface = "I" + name.Replace(NamingConventions.DbServiceSuffix, "Service");

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
