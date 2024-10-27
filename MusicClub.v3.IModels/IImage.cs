using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicClub.v3.IModels
{
    public interface IImage
    {
        string Alt { get; set; }
        byte[] Content { get; set; }
        string ContentType { get; set; }
    }
}
