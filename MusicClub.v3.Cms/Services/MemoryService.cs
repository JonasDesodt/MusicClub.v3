using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.Cms.Services
{
    public class MemoryService
    {
        public const int DefaultPage = 1;
        public const int DefaultPageSize = 12;


        public static PaginationRequest GetDefaultPaginationRequest()
        {
            return new PaginationRequest
            {
                Page = DefaultPage,
                PageSize = DefaultPageSize
            };
        }

        public bool HasUnsavedData { get; set; } = false;
        public event EventHandler? OnConfirmationRequested;

        public void RequestConfirmation()
        {
            OnConfirmationRequested?.Invoke(this, EventArgs.Empty);
        }

        public string? NavigationRequest { get; set; }

        private readonly Dictionary<Type, object> _memory = [];

        public void Set(object value)
        {
            var type = value.GetType();

            _memory[type] = value;
        }

        public TType? Get<TType>() where TType : class
        {
            if (_memory.TryGetValue(typeof(TType), out object? value))
            {
                return value as TType;
            }

            return null;
        }

        public void Clear() => _memory.Clear();

        public bool Clear<TType>() where TType : class
        {
            return _memory.Remove(typeof(TType));
        }
    }
}
