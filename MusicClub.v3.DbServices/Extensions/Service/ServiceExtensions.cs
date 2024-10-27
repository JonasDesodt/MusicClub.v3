using Microsoft.EntityFrameworkCore;

namespace MusicClub.v3.DbServices.Extensions.Service
{
    internal static class ServiceExtensions
    {
        public static async Task<bool> HasReferenceToLineup(this DbSet<DbCore.Models.Service> services, int id)
        {
            return await services.AnyAsync(a => a.LineupId == id);
        }

        //public static IQueryable<Service> IncludeAll(this IQueryable<Service> query)
        //{
        //    return query.Include(a => a.Services)
        //                .Include(a => a.Jobs)
        //                .Include(a => a.Lineup)
        //                .Include(a => a.Performances);
        //}

        //public static IQueryable<ActResult> ToResults(this IQueryable<Act> query)
        //{
        //    return query.Select(a => new ActResult
        //    {
        //        Name = a.Name,
        //        Title = a.Title,
        //        PerformancesCount = a.Performances.Count,
        //        Created = a.Created,
        //        Id = a.Id,
        //        Image = a.Image != null ? a.Image.ToResult() : null,
        //        Updated = a.Updated,
        //        JobsCount = a.Jobs.Count,
        //        Lineup = a.Lineup!.ToResult() //TODO: temp hack (!), deal w/ null reference
        //    });
        //}
    }
}