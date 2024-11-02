namespace MusicClub.v3.Dto.Transfer
{
    public class Between<T> where T : struct
    {
        public T? From { get; set; }
        public bool IncludeFrom { get; set; } = true;
        public T? To { get; set; }
        public bool IncludeTo { get; set; } = true;
    }
}
