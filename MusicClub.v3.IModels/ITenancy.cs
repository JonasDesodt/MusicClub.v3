namespace MusicClub.v3.IModels
{
    public interface ITenancy 
    {
        int TenantId { get; set; }

        string ApplicationUserId { get; set; }
    }
}
