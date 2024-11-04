namespace MusicClub.v3.DbCore.Models
{
    public class Tenancy
    {
        public int Id { get; set; }
        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }


        public required int TenantId { get; set; }
        public Tenant? Tenant { get; set; }

        public required string ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser {get;set;}
    }
}
