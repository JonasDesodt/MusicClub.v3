using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using MusicClub.v3.SourceGenerators.Shared.Extensions;

namespace MusicClub.v3.SourceGenerators.Shared.Receivers
{
    public class ClassDeclarationSyntaxReceiver : ISyntaxReceiver
    {
        public List<ClassDeclarationSyntax> Classes { get; } = new List<ClassDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax classDeclaration)
            {
                Classes.Add(classDeclaration);
            }
        }

        public IEnumerable<(ClassDeclarationSyntax, ISymbol, IEnumerable<string>)> GetModels(GeneratorExecutionContext context, string attributeName)
        {
            foreach (var classDeclarationSyntax in Classes)
            {
                var classSymbol = context.GetISymbol(classDeclarationSyntax);

                if (classSymbol is null)
                {
                    continue;
                }

                foreach (var attributeData in classSymbol.GetAttributes())
                {
                    if (attributeData.AttributeClass?.Name == attributeName)
                    {
                        yield return (classDeclarationSyntax, classSymbol, attributeData.GetParamStringArrayValues());
                    }
                }
            }
        }


        public IEnumerable<(ClassDeclarationSyntax, IEnumerable<string>)> GetModels(Compilation compilation, string attributeName)
        {
            foreach (var classDeclarationSyntax in Classes)
            {
                var semanticModel = compilation.GetSemanticModel(classDeclarationSyntax.SyntaxTree);
                var classSymbol = semanticModel.GetDeclaredSymbol(classDeclarationSyntax);
                if (classSymbol is null)
                {
                    continue;
                }

                foreach (var attributeData in classSymbol.GetAttributes())
                {
                    if (attributeData.AttributeClass?.Name == attributeName)
                    {
                        yield return (classDeclarationSyntax, attributeData.GetParamStringArrayValues());
                    }
                }
            }
        }

        public IEnumerable<(ClassDeclarationSyntax, ISymbol, AttributeData)> GetClassDeclarationSyntaxWithAttributeData(GeneratorExecutionContext context, string attributeName)
        {
            foreach (var classDeclarationSyntax in Classes)
            {
                var classSymbol = context.GetISymbol(classDeclarationSyntax);

                if (classSymbol is null)
                {
                    continue;
                }

                foreach (var attributeData in classSymbol.GetAttributes())
                {
                    if (attributeData.AttributeClass?.Name == attributeName)
                    {
                        yield return (classDeclarationSyntax, classSymbol, attributeData);
                    }
                }
            }
        }


        public IEnumerable<(ClassDeclarationSyntax, AttributeData)> GetClassDeclarationSyntaxWithAttributeData(Compilation compilation, string attributeName)
        {
            foreach (var classDeclarationSyntax in Classes)
            {
                var semanticModel = compilation.GetSemanticModel(classDeclarationSyntax.SyntaxTree);
                var classSymbol = semanticModel.GetDeclaredSymbol(classDeclarationSyntax);


                if (classSymbol is null)
                {
                    continue;
                }

                foreach (var attributeData in classSymbol.GetAttributes())
                {
                    if (attributeData.AttributeClass?.Name == attributeName)
                    {
                        yield return (classDeclarationSyntax, attributeData);
                    }
                }
            }
        }
    }
}