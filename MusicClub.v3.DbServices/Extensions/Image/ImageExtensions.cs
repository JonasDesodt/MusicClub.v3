using Microsoft.EntityFrameworkCore;
using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Enums;
using MusicClub.v3.Dto.Filter.Request;

namespace MusicClub.v3.DbServices.Extensions.Image
{
    internal static class ImageExtensions
    {
        public static ImageDataResponse ToResponse(this DbCore.Models.Image image)
        {
            return new ImageDataResponse
            {
                Alt = image.Alt,
                ContentType = image.ContentType,
                Created = image.Created,
                Updated = image.Updated,
                Id = image.Id,
                ActsCount = image.Acts.Count,
                ArtistsCount = image.Artists.Count,
                LineupsCount = image.Lineups.Count,
                PeopleCount = image.People.Count,
                PerformancesCount = image.Performances.Count
            };
        }

        public static IQueryable<ImageDataResponse> ToResponses(this IQueryable<DbCore.Models.Image> query)
        {
            return query.Select(i => new ImageDataResponse
            {
                Alt = i.Alt,
                ContentType = i.ContentType,
                Created = i.Created,
                Updated = i.Updated,
                Id = i.Id,
                ActsCount = i.Acts.Count,
                ArtistsCount = i.Artists.Count,
                LineupsCount = i.Lineups.Count,
                PeopleCount = i.People.Count,
                PerformancesCount = i.Performances.Count
            });
        }

        public static IQueryable<DbCore.Models.Image> IncludeAll(this IQueryable<DbCore.Models.Image> query)
        {
            return query.Include(q => q.Artists)
                        .Include(q => q.Acts)
                        .Include(q => q.People)
                        .Include(q => q.Performances)
                        .Include(q => q.Lineups);
        }

        public static IQueryable<DbCore.Models.Image> Filter(this IQueryable<DbCore.Models.Image> images, ImageFilterRequest filterRequest)
        {
            if (!string.IsNullOrWhiteSpace(filterRequest.Alt))
            {
                images = images.Where(a => a.Alt != null && a.Alt.ToLower().Contains(filterRequest.Alt.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filterRequest.SortProperty))
            {
                if (filterRequest.SortDirection is SortDirection.Descending)
                {
                    images = filterRequest.SortProperty switch
                    {
                        nameof(ImageDataResponse.Alt) => images.OrderByDescending(a => a.Alt),
                        _ => images.OrderByDescending(a => a.Id),
                    };
                }
                else
                {
                    images = filterRequest.SortProperty switch
                    {
                        nameof(ImageDataResponse.Alt) => images.OrderBy(a => a.Alt),
                        _ => images.OrderBy(a => a.Id),
                    };
                }
            }

            return images;
        }

        public static DbCore.Models.Image Update(this DbCore.Models.Image image, ImageDbDataRequest request)
        {
            image.Alt = request.Alt;
            image.Updated = DateTime.Now;

            if (request.Content is not null && request.ContentType is not null)
            {
                image.Content = request.Content;
                image.ContentType = request.ContentType;
            }

            return image;
        }
    }
}