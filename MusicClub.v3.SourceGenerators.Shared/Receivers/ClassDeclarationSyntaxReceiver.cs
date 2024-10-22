using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;
using System;

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

        public IEnumerable<(ClassDeclarationSyntax, IEnumerable<string>)> GetModels(Compilation compilation, string attributeConstructorName)
        {
            foreach (var classDeclarationSyntax in Classes)
            {
                foreach (var attributeListSyntax in classDeclarationSyntax.AttributeLists)
                {
                    foreach (var attributeSyntax in attributeListSyntax.Attributes)
                    {
                        var semanticModel = compilation.GetSemanticModel(attributeSyntax.SyntaxTree);
                        var attributeSymbol = semanticModel.GetSymbolInfo(attributeSyntax).Symbol;

                        if (attributeSymbol != null)
                        {
                            if (attributeSymbol.ToString() == attributeConstructorName)
                            {
                                yield return (classDeclarationSyntax,
                                attributeSyntax.ArgumentList.Arguments
                                .Select(a => a.Expression is LiteralExpressionSyntax literalExpression
                                ? literalExpression.Token.ValueText
                                : a?.ToString() ?? "unknown"));
                            }
                        }
                    }
                }
            }
        }
    }
}