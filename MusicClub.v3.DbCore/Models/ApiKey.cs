using MusicClub.v3.DbCore.SourceGeneratorAttributes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MusicClub.v3.DbCore.Models
{
    public class ApiKey : IApiKey
    {
        public int Id { get; set; }
        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; } // todo => try to get rid of this prop, only here because of the sourcegenerator

        public DateTime? Archived { get; set; } = null;
        public required byte[] HashedApiKey { get; set; }
        public required byte[] Salt { get; set; } 
        

        public required int TenantId { get; set; }
        public Tenant? Tenant { get; set; }

    }
}
