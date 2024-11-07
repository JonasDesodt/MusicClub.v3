using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicClub.v3.Dto.Auth
{
    public class TokenAuthRequest
    {
        [Required]
        public required string Emailaddress { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
