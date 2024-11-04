using MusicClub.v3.DbCore;
using MusicClub.v3.DbCore.Services;

namespace MusicClub.v3.Api.Middleware
{
    public class ApiKeyMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("Api-Key", out var extractedApiKey))
            {
                context.Items["API_Key_Authenticated"] = false;
            }

            if (!context.Request.Headers.TryGetValue("Application-Name", out var extractedApplicationName))
            {
                context.Items["API_Key_Authenticated"] = false;
            }

            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MusicClubDbContext>();

                var tenant = dbContext.Tenants.SingleOrDefault(t => t.Name == extractedApplicationName.ToString() && t.ApiKey == extractedApiKey.ToString());
                
                if (tenant is null)
                {
                    context.Items["API_Key_Authenticated"] = false;
                }
                else
                {
                    context.Items["API_Key_Authenticated"] = tenant.Id;
                }
            }

            await next(context);
        }
    }
}