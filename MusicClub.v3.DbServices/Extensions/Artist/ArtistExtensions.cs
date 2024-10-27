using Microsoft.EntityFrameworkCore;
using MusicClub.v3.Dto.Enums;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.DbServices.Extensions.Artist;
using MusicClub.v3.DbServices.Extensions.Image;
using MusicClub.v3.DbServices.Extensions.Person;

namespace MusicClub.v3.DbServices.Extensions.Artist
{
    internal static class ArtistExtensions
    {
        public static async Task<bool> HasReferenceToPerson(this DbSet<DbCore.Models.Artist> artists, int id)
        {
            return await artists.AnyAsync(a => a.PersonId == id);
        }

        public static IQueryable<DbCore.Models.Artist> IncludeAll(this IQueryable<DbCore.Models.Artist> query)
        {
            return query.Include(a => a.Image)
                        .Include(a => a.Person)
                        .Include(a => a.Performances);
        }

        public static IQueryable<ArtistDataResponse> ToResponses(this IQueryable<DbCore.Models.Artist> query)
        {
            return query.Select(a => new ArtistDataResponse
            {
                Alias = a.Alias,
                PerformancesCount = a.Performances.Count,
                Created = a.Created,
                Id = a.Id,
                ImageDataResponse = a.Image != null ? a.Image.ToResponse() : null,
                Updated = a.Updated,
                PersonDataResponse = a.Person != null ? a.Person.ToResponse() : null!
            });
        }

        public static IQueryable<DbCore.Models.Artist> Filter(this IQueryable<DbCore.Models.Artist> artists, ArtistFilterRequest filterRequest)
        {

            if (!string.IsNullOrWhiteSpace(filterRequest.Alias))
            {
                artists = artists.Where(a => a.Alias != null && a.Alias.ToLower().Contains(filterRequest.Alias.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filterRequest.SortProperty))
            {
                if (filterRequest.SortDirection is SortDirection.Descending)
                {
                    artists = filterRequest.SortProperty switch
                    {
                        nameof(DbCore.Models.Artist.Alias) => artists.OrderByDescending(a => a.Alias),
                        nameof(DbCore.Models.Person.Firstname) => artists.OrderByDescending(a => a.Person != null ? a.Person.Firstname : a.Alias),
                        nameof(DbCore.Models.Person.Lastname) => artists.OrderByDescending(a => a.Person != null ? a.Person.Lastname : a.Alias),

                        _ => artists.OrderByDescending(a => a.Id),
                    };
                }
                else
                {
                    artists = filterRequest.SortProperty switch
                    {
                        nameof(DbCore.Models.Artist.Alias) => artists.OrderBy(a => a.Alias),
                        nameof(DbCore.Models.Person.Firstname) => artists.OrderBy(a => a.Person != null ? a.Person.Firstname : a.Alias),
                        nameof(DbCore.Models.Person.Lastname) => artists.OrderBy(a => a.Person != null ? a.Person.Lastname : a.Alias),

                        _ => artists.OrderBy(a => a.Id),
                    };
                }
            }

            return artists;
        }


        public static DbCore.Models.Artist Update(this DbCore.Models.Artist artist, ArtistDataRequest dataRequest)
        {
            artist.PersonId = dataRequest.PersonId;
            artist.ImageId = dataRequest.ImageId;
            artist.Updated = DateTime.UtcNow;
            artist.Alias = dataRequest.Alias;

            return artist;
        }

        public static ArtistDataResponse ToResponse(this DbCore.Models.Artist artist)
        {
            return new ArtistDataResponse
            {
                Alias = artist.Alias,
                Created = artist.Created,
                Id = artist.Id,
                ImageDataResponse = artist.Image?.ToResponse(),
                Updated = artist.Updated,
                PersonDataResponse = artist.Person != null ? artist.Person.ToResponse() : null!,
                PerformancesCount = artist.Performances.Count
            };
        }
    }
}