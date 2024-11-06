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

            }

            await next(httpContext);
        }
    }
}
