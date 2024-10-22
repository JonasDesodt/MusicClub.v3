using Microsoft.CodeAnalysis;
using MusicClub.v3.SourceGenerators.Shared.Receivers;
using System;

namespace MusicClub.v3.SourceGenerators.Api
{
    [Generator]
    public class ControllersSourceGenerator : ISourceGenerator
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

            foreach(var (classDeclarationSyntax, models) in receiver.GetModels(context.Compilation, $"{Constants.AttributesNamespace}.{Constants.GenerateControllersAttributeName}.{Constants.GenerateControllersAttributeName}({Constants.GenerateControllersAttributeParamsTypes})"))
            {
                foreach(var model in models)
                {
                    context.AddSource($"{model}Controller{Constants.FileExtension}", $"namespace {Constants.ApiControllersNamespace} {{ public class {model}Controller : {Constants.ApiControllerName} {{ }} }}");
                }
            }      
        }
    }
}
