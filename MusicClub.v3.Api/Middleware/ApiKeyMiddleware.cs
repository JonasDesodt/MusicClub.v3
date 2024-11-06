using Microsoft.EntityFrameworkCore;
using MusicClub.v3.DbCore;
using MusicClub.v3.DbServices.Helpers;

namespace MusicClub.v3.Api.Middleware
{
    public class ApiKeyMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context, MusicClubDbContext dbContext)
        {
            context.Items["API_Key_Authenticated"] = false;

            if (context.Request.Headers.TryGetValue("Api-Key", out var extractedApiKey)
                && context.Request.Headers.TryGetValue("Application-Name", out var extractedApplicationName)
                && dbContext.Tenants.SingleOrDefault(t => t.Name == extractedApplicationName.ToString()) is { } tenant
                && dbContext.ApiKeys.FirstOrDefault(x => x.TenantId == tenant.Id && x.Archived == null) is { } apiKey
                // todo => add check if the key is not too old? eg 1 year
                && ApiKeyHelper.ValidateApiKey(apiKey, extractedApiKey.ToString()))
            {
                context.Items["API_Key_Authenticated"] = tenant.Id;
            }

            await next(context);
        }
    }
}