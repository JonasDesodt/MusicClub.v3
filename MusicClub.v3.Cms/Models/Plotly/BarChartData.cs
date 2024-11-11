namespace MusicClub.v3.Cms.Models.Plotly
{
    public class BarChartData
    {
        public List<string> X { get; set; } = [];
        public List<int> Y { get; set; } = [];
        public required string Type { get; set; }
        public required string Orientation { get; set; }
        public required Marker Marker { get; set; }
    }
}
