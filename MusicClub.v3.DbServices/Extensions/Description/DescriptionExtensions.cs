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

        public static IQueryable<DescriptionDataResponse> ToResponses(this IQueryable<DbCore.Models.Description> query)
        {
            return query.Select(a => new DescriptionDataResponse
            {
                ActsCount = a.Acts.Count,
                Created = a.Created,
                Updated = a.Updated,
                DescriptionTranslationsCount = a.DescriptionTranslations.Count,
                Id = a.Id
            });
        }

        public static IQueryable<DbCore.Models.Description> IncludeAll(this IQueryable<DbCore.Models.Description> query)
        {
            return query;
        }
    }
}
