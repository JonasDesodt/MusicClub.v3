using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

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
    }
}