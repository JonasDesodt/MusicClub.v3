namespace MusicClub.v3.Cms.Models.Plotly
{
    public class LineGraphData
    {
        public List<string> X { get; set; } = [];

        public List<int> Y { get; set; } = [];

        public required string Mode { get; set; }
    }
}
