using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicClub.v3.Dto.Data.Response
{
    public partial class LineupDataResponse
    {
        public required int ActsCount { get; set; }

        public required int ServicesCount { get; set; }
    }
}
