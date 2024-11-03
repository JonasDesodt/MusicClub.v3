using Microsoft.EntityFrameworkCore;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Enums;
using MusicClub.v3.Dto.Filter.Request;

namespace MusicClub.v3.DbServices.Extensions.Language
{
    internal static class LanguageExtensions
    {
        public static IQueryable<DbCore.Models.Language> IncludeAll(this IQueryable<DbCore.Models.Language> query)
        {
            return query.Include(l => l.DescriptionTranslations);
        }

        public static IQueryable<LanguageDataResponse> ToResponses(this IQueryable<DbCore.Models.Language> query)
        {
            return query.Select(l => new LanguageDataResponse
            {
                Created = l.Created,
                Id = l.Id,
                Updated = l.Updated,
                DescriptionTranslationsCount = l.DescriptionTranslations.Count,
                Identifier = l.Identifier
            });
        }

        public static LanguageDataResponse ToResponse(this DbCore.Models.Language language)
        {
            return new LanguageDataResponse
            {
                Created = language.Created,
                Id = language.Id,
                Updated = language.Updated,
                DescriptionTranslationsCount = language.DescriptionTranslations.Count,
                Identifier = language.Identifier
            };
        }

        public static IQueryable<v3.DbCore.Models.Language> Filter(this IQueryable<v3.DbCore.Models.Language> query, LanguageFilterRequest filterRequest)
        {
            if (!string.IsNullOrWhiteSpace(filterRequest.Identifier))
            {
                query = query.Where(a => a.Identifier.ToLower().Contains(filterRequest.Identifier.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filterRequest.SortProperty))
            {
                if (filterRequest.SortDirection is SortDirection.Descending)
                {
                    query = filterRequest.SortProperty switch
                    {
                        nameof(v3.DbCore.Models.Language.Created) => query.OrderByDescending(l => l.Created),
                        nameof(v3.DbCore.Models.Language.Updated) => query.OrderByDescending(l => l.Updated),
                        nameof(v3.DbCore.Models.Language.Identifier) => query.OrderByDescending(l => l.Identifier),
                        _ => query.OrderByDescending(l => l.Id),
                    };
                }
                else
                {
                    query = filterRequest.SortProperty switch
                    {
                        nameof(v3.DbCore.Models.Language.Created) => query.OrderBy(l => l.Created),
                        nameof(v3.DbCore.Models.Language.Updated) => query.OrderBy(l => l.Updated),
                        nameof(v3.DbCore.Models.Language.Identifier) => query.OrderBy(l => l.Identifier),
                        _ => query.OrderBy(l => l.Id),
                    };
                }
            }

            return query;
        }
    }
}
