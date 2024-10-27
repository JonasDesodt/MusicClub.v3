namespace MusicClub.v3.IModels
{
    public interface IJob
    {
        string Description { get; set; }
        int WorkerId { get; set; }
        int FunctionId { get; set; }
        int ActId { get; set; }
    }
}
