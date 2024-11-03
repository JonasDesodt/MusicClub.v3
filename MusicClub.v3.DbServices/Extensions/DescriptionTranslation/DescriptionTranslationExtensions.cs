using Microsoft.EntityFrameworkCore;
using MusicClub.v3.DbServices.Extensions.Description;
using MusicClub.v3.DbServices.Extensions.Language;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Enums;
using MusicClub.v3.Dto.Filter.Request;

namespace MusicClub.v3.DbServices.Extensions.DescriptionTranslation
{
    internal static class DescriptionTranslationExtensions
    {
        public static IQueryable<DbCore.Models.DescriptionTranslation> IncludeAll(this IQueryable<DbCore.Models.DescriptionTranslation> query)
        {
            return query.Include(d => d.Description).Include(d => d.Language);
        }

        public static IQueryable<DescriptionTranslationDataResponse> ToResponses(this IQueryable<DbCore.Models.DescriptionTranslation> query)
        {
            return query.Select(d => new DescriptionTranslationDataResponse
            {
                Created = d.Created,
                Id = d.Id,
                Updated = d.Updated,
                DescriptionDataResponse = d.Description != null ? d.Description.ToResponse() : null!, //to do => deal with null reference 
                LanguageDataResponse = d.Language != null ? d.Language.ToResponse() : null!,
                Text = d.Text
            });
        }

        public static DescriptionTranslationDataResponse ToResponse(this DbCore.Models.DescriptionTranslation descriptionTranslation)
        {
            return new DescriptionTranslationDataResponse
            {
                Created = descriptionTranslation.Created,
                Id = descriptionTranslation.Id,
                Updated = descriptionTranslation.Updated,
                DescriptionDataResponse = descriptionTranslation.Description != null ? descriptionTranslation.Description.ToResponse() : null!, //to do => deal with null reference 
                LanguageDataResponse = descriptionTranslation.Language != null ? descriptionTranslation.Language.ToResponse() : null!,
                Text = descriptionTranslation.Text
            };
        }

        public static IQueryable<v3.DbCore.Models.DescriptionTranslation> Filter(this IQueryable<v3.DbCore.Models.DescriptionTranslation> query, DescriptionTranslationFilterRequest filterRequest)
        {

            if (filterRequest.LanguageId > 0)
            {
                query = query.Where(a => a.LanguageId == filterRequest.LanguageId);
            }

            if (filterRequest.DescriptionId > 0)
            {
                query = query.Where(a => a.DescriptionId == filterRequest.DescriptionId);
            }



            if (!string.IsNullOrWhiteSpace(filterRequest.SortProperty))
            {
                if (filterRequest.SortDirection is SortDirection.Descending)
                {
                    query = filterRequest.SortProperty switch
                    {
                        nameof(v3.DbCore.Models.DescriptionTranslation.Created) => query.OrderByDescending(l => l.Created),
                        nameof(v3.DbCore.Models.DescriptionTranslation.Updated) => query.OrderByDescending(l => l.Updated),
                        nameof(v3.DbCore.Models.DescriptionTranslation.Language) => query.OrderByDescending(l => l.LanguageId),
                        nameof(v3.DbCore.Models.DescriptionTranslation.Description) => query.OrderByDescending(l => l.DescriptionId),
                        _ => query.OrderByDescending(l => l.Id),
                    };
                }
                else
                {
                    query = filterRequest.SortProperty switch
                    {
                        nameof(v3.DbCore.Models.DescriptionTranslation.Created) => query.OrderBy(l => l.Created),
                        nameof(v3.DbCore.Models.DescriptionTranslation.Updated) => query.OrderBy(l => l.Updated),
                        nameof(v3.DbCore.Models.DescriptionTranslation.Language) => query.OrderBy(l => l.LanguageId),
                        nameof(v3.DbCore.Models.DescriptionTranslation.Description) => query.OrderBy(l => l.DescriptionId),
                        _ => query.OrderBy(l => l.Id),
                    };
                }
            }

            return query;
        }
    }
}
