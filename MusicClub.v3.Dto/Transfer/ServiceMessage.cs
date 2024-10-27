using MusicClub.v3.Dto.Enums;

namespace MusicClub.v3.Dto.Transfer
{
    public class ServiceMessage
    {
        public required ErrorCode Code { get; set; }

        public required string Description { get; set; }
    }

    public class ServiceMessages : List<ServiceMessage>
    {
        public bool HasMessage => Count > 0;
    }
}