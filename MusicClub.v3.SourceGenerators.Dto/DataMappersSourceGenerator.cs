using Microsoft.CodeAnalysis;
using MusicClub.v3.SourceGenerators.Shared.Constants;
using MusicClub.v3.SourceGenerators.Shared.Extensions;
using MusicClub.v3.SourceGenerators.Shared.Receivers;
using System.Text.RegularExpressions;

namespace MusicClub.v3.SourceGenerators.Dto
{
    [Generator]
    internal class DataMappersSourceGenerator : ISourceGenerator
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

            foreach (var (requestClassDeclarationSyntax, requestClassSymbol, requestClassAttributeData) in receiver.GetClassDeclarationSyntaxWithAttributeData(context, "GenerateDataMappers"))
            {
                var constants = requestClassAttributeData.GetConstants(new string[]
                {
                    "ValidationPattern",
                    "ClassNameReplacePattern",
                    "ClassNameReplacement",
                    "ClassNameSuffix",
                    "NamespaceReplacePattern",
                    "NamespaceReplacement",
                    "Request",
                    "Response"
                });

                var validationPattern = constants["ValidationPattern"];

                var dataRequestClassName = requestClassSymbol.GetClassName();

                if (!Regex.IsMatch(dataRequestClassName, validationPattern))
                {
                    continue;
                }

                var classNameReplacePattern = constants["ClassNameReplacePattern"];
                var classNameReplacement = constants["ClassNameReplacement"];

                var dataResponseClassname = Regex.Replace(dataRequestClassName, classNameReplacePattern, classNameReplacement);

                var classNameSuffix = constants["ClassNameSuffix"];
                
                var dataRequestExtensionsClassname = dataRequestClassName + classNameSuffix;
                var dataResponseExtensionsClassName =  dataResponseClassname + classNameSuffix;

                var namespaceReplacePattern = constants["NamespaceReplacePattern"];
                var namespaceReplacement = constants["NamespaceReplacement"];
 
                if (!new Regex(namespaceReplacePattern).TryReplace(context.GetNamespace(requestClassDeclarationSyntax), namespaceReplacement, out string baseNamespace))
                {
                    continue;
                }

                var request = constants["Request"];
                var response = constants["Response"];

                var dataRequestExtensionsNamespace = baseNamespace + "." + request;
                var dataResponseExtensionsNamespace = baseNamespace + "." + response;

                context.AddSource(dataRequestExtensionsClassname + NamingConventions.FileExtension, "//" + dataRequestExtensionsNamespace);
                //context.AddSource(dataResponseExtensionsClassName + NamingConventions.FileExtension, "//" + dataResponseExtensionsNamespace);
            }


        }
    }
}
