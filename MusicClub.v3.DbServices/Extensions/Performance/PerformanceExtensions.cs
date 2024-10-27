using Microsoft.EntityFrameworkCore;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.DbServices.Extensions.Image;
using MusicClub.v3.DbServices.Extensions.Bandname;
using MusicClub.v3.DbServices.Extensions.Artist;
using MusicClub.v3.Dto.Enums;
using MusicClub.v3.DbServices.Extensions.Act;

namespace MusicClub.v3.DbServices.Extensions.Performance
{
    internal static class PerformanceExtensions
    {
        public static async Task<bool> HasReferenceToArtist(this DbSet<v3.DbCore.Models.Performance> performances, int id)
        {
            return await performances.AnyAsync(a => a.ArtistId == id);
        }

        public static async Task<bool> HasReferenceToAct(this DbSet<v3.DbCore.Models.Performance> performances, int id)
        {
            return await performances.AnyAsync(a => a.ActId == id);
        }

        public static IQueryable<v3.DbCore.Models.Performance> IncludeAll(this IQueryable<v3.DbCore.Models.Performance> query)
        {
            return query.Include(p => p.Image)
                        .Include(p => p.Artist).ThenInclude(a => a != null ? a.Person: null)
                        .Include(p => p.Bandname)
                        .Include(p => p.Act).ThenInclude(a => a != null ? a.Lineup : null);
        }

        public static IQueryable<PerformanceDataResponse> ToResponses(this IQueryable<v3.DbCore.Models.Performance> query)
        {
            return query.Select(p => new PerformanceDataResponse
            {
                Created = p.Created,
                Id = p.Id,
                ImageDataResponse = p.Image != null ? p.Image.ToResponse() : null,
                Updated = p.Updated,
                Instrument = p.Instrument,
                BandnameDataResponse = p.Bandname != null ? p.Bandname.ToResponse() : null,
                ActDataResponse = p.Act!.ToResponse(), //todo: temp hack, deal with null reference    
                ArtistDataResponse = p.Artist!.ToResponse()  //todo: temp hack, deal with null reference    
            });
        }

        public static IQueryable<v3.DbCore.Models.Performance> Filter(this IQueryable<v3.DbCore.Models.Performance> query, PerformanceFilterRequest filterRequest)
        {
            if (!string.IsNullOrWhiteSpace(filterRequest.Instrument))
            {
                query = query.Where(a => a.Instrument != null && a.Instrument.ToLower().Contains(filterRequest.Instrument.ToLower()));
            }

            if (filterRequest.ImageId > 0)
            {
                query = query.Where(a => a.ImageId != null && a.ImageId == filterRequest.ImageId);
            }

            if (filterRequest.ArtistId > 0)
            {
                query = query.Where(a => a.ArtistId == filterRequest.ArtistId);
            }

            if (filterRequest.BandnameId > 0)
            {
                query = query.Where(a => a.BandnameId != null && a.BandnameId == filterRequest.BandnameId);
            }

            if (filterRequest.ActId > 0)
            {
                query = query.Where(a => a.ActId == filterRequest.ActId);
            }

            if (!string.IsNullOrWhiteSpace(filterRequest.SortProperty))
            {
                if (filterRequest.SortDirection is SortDirection.Descending)
                {
                    query = filterRequest.SortProperty switch
                    {
                        nameof(v3.DbCore.Models.Performance.Instrument) => query.OrderByDescending(l => l.Instrument),
                        nameof(v3.DbCore.Models.Performance.Image) => query.OrderByDescending(l => l.Image != null ? l.Image.Alt : null),
                        nameof(v3.DbCore.Models.Performance.Created) => query.OrderByDescending(l => l.Created),
                        nameof(v3.DbCore.Models.Performance.Updated) => query.OrderByDescending(l => l.Updated),
                        nameof(v3.DbCore.Models.Performance.Act) => query.OrderByDescending(l => l.ActId),
                        nameof(v3.DbCore.Models.Performance.Artist) => query.OrderByDescending(l => l.Artist),
                        nameof(v3.DbCore.Models.Performance.Bandname) => query.OrderByDescending(l => l.Bandname != null ? l.Bandname.Name : null),
                        _ => query.OrderByDescending(l => l.Id),
                    };
                }
                else
                {
                    query = filterRequest.SortProperty switch
                    {
                        nameof(v3.DbCore.Models.Performance.Instrument) => query.OrderBy(l => l.Instrument),
                        nameof(v3.DbCore.Models.Performance.Image) => query.OrderBy(l => l.Image != null ? l.Image.Alt : null),
                        nameof(v3.DbCore.Models.Performance.Created) => query.OrderBy(l => l.Created),
                        nameof(v3.DbCore.Models.Performance.Updated) => query.OrderBy(l => l.Updated),
                        nameof(v3.DbCore.Models.Performance.Act) => query.OrderBy(l => l.ActId),
                        nameof(v3.DbCore.Models.Performance.Artist) => query.OrderBy(l => l.Artist),
                        nameof(v3.DbCore.Models.Performance.Bandname) => query.OrderBy(l => l.Bandname != null ? l.Bandname.Name : null),
                        _ => query.OrderBy(l => l.Id),
                    };
                }
            }

            return query;
        }

        public static v3.DbCore.Models.Performance Update(this v3.DbCore.Models.Performance performance, PerformanceDataRequest request)
        {
            performance.ImageId = request.ImageId;
            performance.Updated = DateTime.UtcNow;
            performance.ActId = request.ActId;
            performance.ArtistId = request.ArtistId;
            performance.ActId = request.ActId;
            performance.BandnameId = request.BandnameId;
            performance.Instrument = request.Instrument;

            return performance;
        }

        public static PerformanceDataResponse ToResponse(this v3.DbCore.Models.Performance performance)
        {
            return new PerformanceDataResponse
            {
                Created = performance.Created,
                Id = performance.Id,
                ImageDataResponse = performance.Image != null ? performance.Image.ToResponse() : null,
                Updated = performance.Updated,
                Instrument = performance.Instrument,
                BandnameDataResponse = performance.Bandname != null ? performance.Bandname.ToResponse() : null,
                ActDataResponse = performance.Act!.ToResponse(), //todo: temp hack, deal with null reference    
                ArtistDataResponse = performance.Artist!.ToResponse()  //todo: temp hack, deal with null reference    
            };
        }
    }
}