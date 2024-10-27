using Microsoft.EntityFrameworkCore;

namespace MusicClub.v3.DbServices.Extensions.Worker
{
    internal static class WorkerExtensions
    {
        public static async Task<bool> HasReferenceToPerson(this DbSet<v3.DbCore.Models.Worker> workers, int id)
        {
            return await workers.AnyAsync(a => a.PersonId == id);
        }
    }
}