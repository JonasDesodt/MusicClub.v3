using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicClub.v3.DbCore;

namespace MusicClub.v3.Api.Middleware
{
    public class TenantMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext httpContext, MusicClubDbContext dbContext)
        {
            if (httpContext.Items["API_Key_Authenticated"] is int id)
            {
                dbContext.CurrentTenantId = id;
            }
            else if (httpContext.User.Identity?.IsAuthenticated is true)
            {
                //no fallbacks => if the user is authenticated, that means the email is in the db + ensure there is never an appUser without tenancy

                var email = httpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                var appUser = dbContext.Users.Single(x => x.Email == email);

                var tenancy = dbContext.Tenancies.Include(t => t.Tenant).First(x => x.ApplicationUserId == appUser.Id);

                dbContext.CurrentTenantId = tenancy.Tenant?.Id ?? 0; //worst case: the user gets no data, but this should never happen => see previous comment
            }

            await next(httpContext);
        }
    }
}
