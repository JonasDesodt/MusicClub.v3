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

            foreach (var (requestClassDeclarationSyntax, attributeData) in receiver.GetClassDeclarationSyntaxWithAttributeData(context.Compilation, "GenerateDataMappers"))
            {
                if (!(attributeData.GetPropertyValue("ValidationPattern") is string validationPattern))
                {
                    continue;
                }

                var dataRequestClassName = context.GetClassName(requestClassDeclarationSyntax);

                if (!Regex.IsMatch(dataRequestClassName, validationPattern))
                {
                    continue;
                }

                if (!(attributeData.GetPropertyValue("ClassNameReplacePattern") is string classNameReplacePattern))
                {
                    continue;
                }
                if (!(attributeData.GetPropertyValue("ClassNameReplacement") is string classNameReplacement))
                {
                    continue;
                }

                var dataResponseClassname = Regex.Replace(dataRequestClassName, classNameReplacePattern, classNameReplacement);

                if (!(attributeData.GetPropertyValue("ClassNameSuffix") is string classNameSuffix))
                {
                    continue;
                }

                var dataRequestExtensionsClassname = dataRequestClassName + classNameSuffix;
                var dataResponseExtensionsClassName =  dataResponseClassname + classNameSuffix;

                if (!(attributeData.GetPropertyValue("NamespaceReplacePattern") is string namespaceReplacePattern))
                {
                    continue;
                }
                if (!(attributeData.GetPropertyValue("NamespaceReplacement") is string namespaceReplacement))
                {
                    continue;
                }

                if (!new Regex(namespaceReplacePattern).TryReplace(context.GetNamespace(requestClassDeclarationSyntax), namespaceReplacement, out string baseNamespace))
                {
                    continue;
                }

                if (!(attributeData.GetPropertyValue("Request") is string request))
                {
                    continue;
                }
                if (!(attributeData.GetPropertyValue("Response") is string response))
                {
                    continue;
                }

                var dataRequestExtensionsNamespace = baseNamespace + "." + request;
                var dataResponseExtensionsNamespace = baseNamespace + "." + response;

                context.AddSource(dataRequestExtensionsClassname + NamingConventions.FileExtension, "//" + dataRequestExtensionsNamespace);
                //context.AddSource(dataResponseExtensionsClassName + NamingConventions.FileExtension, "//" + dataResponseExtensionsNamespace);
            }


        }
    }
}
