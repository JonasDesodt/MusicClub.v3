using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicClub.v3.Dto.SourceGeneratorAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class GenerateIModelImplementation : Attribute
    {
        public const string ValidationPattern = @$".+{GeneratorConstants.Data}{GeneratorConstants.Request}$";
    }
}
