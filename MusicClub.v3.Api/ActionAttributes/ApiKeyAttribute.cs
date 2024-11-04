using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MusicClub.v3.DbCore;
using MusicClub.v3.DbCore.Models;
using MusicClub.v3.DbCore.Services;

namespace MusicClub.v3.Api.ActionAttributes
{
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Items["API_Key_Authenticated"] is not int id)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Unauthorized access"
                };

                return;
            }      

            var tenantService = context.HttpContext.RequestServices.GetRequiredService<TenantService>();
            tenantService.Id = id;

            await next();
        }
    }
}