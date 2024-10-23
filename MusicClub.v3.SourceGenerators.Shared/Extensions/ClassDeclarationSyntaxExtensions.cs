using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace MusicClub.v3.SourceGenerators.Shared.Extensions
{
    public static class ClassDeclarationSyntaxExtensions
    {
        public static IEnumerable<ParameterSyntax> GetSingleConstructorParameters(this ClassDeclarationSyntax classDeclarationSyntax)
        {
            
            //var constructor = classDeclarationSyntax.Members.OfType<ConstructorDeclarationSyntax>().Single(); // crash if there is more than one constructor

            //var parameters = constructor.ParameterList.Parameters;

            foreach (var parameter in classDeclarationSyntax.ParameterList.Parameters)
            {
                yield return parameter;
            }
        }
    }
}
