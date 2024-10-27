namespace MusicClub.v3.Dto.Transfer
{
    public class ServiceResult<T>
    {
        public T? Data { get; set; }

        public ServiceMessages? Messages { get; set; }
    } 
}