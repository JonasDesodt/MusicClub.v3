namespace MusicClub.v3.DbCore.Mappers.IModel
{
    public static class IApiKeyExtensions
    {
        public static ApiKey ToModel(this IApiKey model, byte[] hashedApiKey, byte[] salt)
        {
            var now = DateTime.UtcNow;

            return new ApiKey
            {
                HashedApiKey = hashedApiKey,
                Salt = salt,
                Created = now,
                Updated = now,
                TenantId = model.TenantId,
            };
        }
    }
}
