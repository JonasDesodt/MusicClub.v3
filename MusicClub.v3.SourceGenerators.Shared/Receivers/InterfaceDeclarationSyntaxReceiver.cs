using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using MusicClub.v3.SourceGenerators.Shared.Extensions;

namespace MusicClub.v3.SourceGenerators.Shared.Receivers
{
    public class InterfaceDeclarationSyntaxReceiver : ISyntaxReceiver
    {
        public List<InterfaceDeclarationSyntax> Interfaces { get; } = new List<InterfaceDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is InterfaceDeclarationSyntax interfaceDeclartionSyntax)
            {
                Interfaces.Add(interfaceDeclartionSyntax);
            }
        }

        public IEnumerable<(InterfaceDeclarationSyntax, ISymbol, IEnumerable<string>)> GetModels(GeneratorExecutionContext context, string attributeName)
        {
            foreach (var interfaceDeclarationSyntax in Interfaces)
            {
                var symbol = context.GetISymbol(interfaceDeclarationSyntax);

                if (symbol is null)
                {
                    continue;
                }

                foreach (var attributeData in symbol.GetAttributes())
                {
                    if (attributeData.AttributeClass?.Name == attributeName)
                    {
                        yield return (interfaceDeclarationSyntax, symbol, attributeData.GetParamStringArrayValues());
                    }
                }
            }
        }

        public IEnumerable<(InterfaceDeclarationSyntax, IEnumerable<string>)> GetModels(Compilation compilation, string attributeName)
        {
            foreach (var interfaceDeclarationSyntax in Interfaces)
            {
                var semanticModel = compilation.GetSemanticModel(interfaceDeclarationSyntax.SyntaxTree);
                var symbol = semanticModel.GetDeclaredSymbol(interfaceDeclarationSyntax);
                if (symbol is null)
                {
                    continue;
                }

                foreach (var attributeData in symbol.GetAttributes())
                {
                    if (attributeData.AttributeClass?.Name == attributeName)
                    {
                        yield return (interfaceDeclarationSyntax, attributeData.GetParamStringArrayValues());
                    }
                }
            }
        }
    }
}