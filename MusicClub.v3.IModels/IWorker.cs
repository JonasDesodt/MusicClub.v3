namespace MusicClub.v3.IModels
{
    public interface IWorker
    {
        bool IsTechnician { get; set; }
        bool IsEmployee { get; set; }

        int PersonId { get; set; }
    }
}
