using MusicClub.v3.Dto.Data.Request;


namespace MusicClub.v3.DbServices.Extensions.Image
{
    public static class ImageDbRequestExtensions
    {
        public static DbCore.Models.Image? ToModel(this ImageDbDataRequest request, int tenantId)
        {
            var now = DateTime.UtcNow;

            if (request.Content is not null && request.ContentType is not null)
            {
                return new DbCore.Models.Image
                {
                    Created = now,
                    Updated = now,
                    Alt = request.Alt,
                    Content = request.Content,
                    ContentType = request.ContentType,
                    TenantId = tenantId
                };
            }
            else
            {
                return null;
            }
        }
    }
}
