using MusicClub.v3.Dto.Data.Response;
using Microsoft.EntityFrameworkCore;
using MusicClub.v3.DbServices.Extensions.Image;
using MusicClub.v3.DbServices.Extensions.Bandname;
using MusicClub.v3.DbServices.Extensions.Band;

namespace MusicClub.v3.DbServices.Extensions.Bandname
{
    public static class BandnameExtensions
    {
        public static BandnameDataResponse ToResponse(this DbCore.Models.Bandname bandname)
        {
            return new BandnameDataResponse
            {
                BandDataResponse = bandname.Band!.ToResponse(), // todo: deal w/ null reference, temp hack
                Created = bandname.Created,
                Id = bandname.Id,
                Name = bandname.Name,
                PerformancesCount = bandname.Performances.Count,
                Updated = bandname.Updated,
                ImageDataResponse = bandname.Image != null ? bandname.Image.ToResponse() : null

            };
        }

        public static IQueryable<DbCore.Models.Bandname> IncludeAll(this IQueryable<DbCore.Models.Bandname> query)
        {
            return query.Include(q => q.Band)
                        .Include(q => q.Image)
                        .Include(q => q.Performances);
        }

        public static IQueryable<BandnameDataResponse> ToResponses(this IQueryable<DbCore.Models.Bandname> query)
        {
            return query.Select(a => new BandnameDataResponse
            {
                BandDataResponse = a.Band!.ToResponse(),// todo: deal with null reference
                Created = a.Created,
                Id = a.Id,
                ImageDataResponse = a.Image != null ? a.Image.ToResponse() : null,
                Name = a.Name,
                PerformancesCount = a.Performances.Count,
                Updated = a.Updated
            });
        }
    }
}
