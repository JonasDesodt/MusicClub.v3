using Microsoft.AspNetCore.Identity;

namespace MusicClub.v3.DbCore.Models
{
    public class ApplicationUser : IdentityUser
    {
        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }

        public required int PersonId { get; set; }
        public Person? Person { get; set; }
    }
}
