namespace MusicClub.v3.DbCore.Services
{
    public class TenantService
    {
        private int _id = 0;

        public int Id
        {
            get => _id;
            set
            {
                if(_id != value)
                {
                    _id = value;

                    OnTenantChanged?.Invoke(this, _id);
                }
            }
        } 

        public EventHandler<int>? OnTenantChanged { get; set; }
    }
}
