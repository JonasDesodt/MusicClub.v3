using Microsoft.EntityFrameworkCore;

namespace MusicClub.v3.DbServices.Extensions
{
    internal static class DbSetExtensions
    {
        public static async Task<bool> Exists<T>(this DbSet<T> dbSet, int id) where T : class 
        {
            if (id <= 0)
            {
                return false;
            }

            if (await dbSet.FindAsync(id) is null)
            {
                return false;
            }

            return true;
        }

        public static async Task<bool> ExistsOrIsNull<T>(this DbSet<T> query, int? id) where T : class
        {
            if (id is null)
            {
                return true;
            }

            return await query.Exists((int)id);
        }
    }
}