using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MusicClub.v3.IModels
{
    public interface IService
    {
        string Description { get; set; }
        int WorkerId { get; set; }
        int FunctionId { get; set; }
        int LineupId { get; set; }
    }
}
