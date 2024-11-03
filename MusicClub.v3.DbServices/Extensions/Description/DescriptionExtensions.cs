using MusicClub.v3.Dto.Data.Response;

namespace MusicClub.v3.DbServices.Extensions.Description
{
    internal static class DescriptionExtensions
    {
        public static DescriptionDataResponse ToResponse(this DbCore.Models.Description description)
        {
            return new DescriptionDataResponse
            {
                ActsCount = description.Acts.Count,
                Created = description.Created,
                Updated = description.Updated,
                DescriptionTranslationsCount = description.DescriptionTranslations.Count,
                Id = description.Id
            };
        }
    }
}
