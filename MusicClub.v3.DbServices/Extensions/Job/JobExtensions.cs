using Microsoft.EntityFrameworkCore;

namespace MusicClub.v3.DbServices.Extensions.Job
{
    internal static class JobExtensions
    {
        public static async Task<bool> HasReferenceToWorker(this DbSet<DbCore.Models.Job> jobs, int id)
        {
            return await jobs.AnyAsync(a => a.WorkerId == id);
        }

        public static async Task<bool> HasReferenceToAct(this DbSet<DbCore.Models.Job> jobs, int id)
        {
            return await jobs.AnyAsync(a => a.ActId == id);
        }
    }
}
