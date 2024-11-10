using Microsoft.AspNetCore.Components;
using MusicClub.v3.Dto.Transfer;
using MusicClub.v3.Abstractions.Services;
using MusicClub.v3.Cms.SourceGeneratorAttributes;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Filter.Response;
using MusicClub.v3.Cms.Services;
using MusicClub.v3.Dto.Filter.Request;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components.Authorization;
using MusicClub.v3.Cms.Models.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace MusicClub.v3.Cms.Controllers
{
    [GenerateDataController]
    public partial class DataController(NavigationManager navigationManager, MemoryService memoryService, AuthenticationStateProvider authenticationStateProvider, IActService actApiService, IPersonService personApiService)/*, IArtistService artistApiService, IPersonService personApiService, IPerformanceService performanceApiService, IImageApiService imageApiService, ILineupService lineupApiService)*/ : DataControllerBase(navigationManager, memoryService, authenticationStateProvider)
    {
        [PreFetch("Act")]
        [SuppressMessage("Style", "IDE0052:Remove unread private member", Justification = "Used by generated code")]
        private readonly IActService _actApiService = actApiService;

        //[PreFetch("Artist")]
        //[SuppressMessage("Style", "IDE0052:Remove unread private member", Justification = "Used by generated code")]
        //private readonly IArtistService _artistApiService = artistApiService;

        //[PreFetch("Image")]
        //[SuppressMessage("Style", "IDE0052:Remove unread private member", Justification = "Used by generated code")]
        //private readonly IImageApiService _imageApiService = imageApiService;

        //[PreFetch("Lineup")]
        //[SuppressMessage("Style", "IDE0052:Remove unread private member", Justification = "Used by generated code")]
        //private readonly ILineupService _lineupApiService = lineupApiService;

        //[PreFetch("Performance")]
        //private readonly IPerformanceService _performanceApiService = performanceApiService;

        //[PreFetch("Person")]
        //[SuppressMessage("Style", "IDE0052:Remove unread private member", Justification = "Used by generated code")]
        //private readonly IPersonService _personApiService = personApiService;

        protected override partial Task<bool> HandleRoute(string route);

        protected override async Task<bool> HandleCustomRoute(string route)
        {
            if (route == "/")
            {
                var state = await authenticationStateProvider.GetAuthenticationStateAsync();
                var user = state.User;
                if (user is null) 
                {
                    return false;
                }
                if(state.User.Claims is not { } claims)
                {
                    return false;
                }
                if(claims.Single(c => c.Type == JwtRegisteredClaimNames.Sub).Value is not { } emailAddress)
                {
                    return false;
                }

                var upcomingActsPagedServiceResult = await Fetch(async () => await _actApiService.GetAll(new PaginationRequest { Page = 1, PageSize = 5 }, new ActFilterRequest { SortProperty = "Start", SortDirection = Dto.Enums.SortDirection.Ascending }));
                var updatedActsPagedServiceResult = await Fetch(async () => await _actApiService.GetAll(new PaginationRequest { Page = 1, PageSize = 5 }, new ActFilterRequest { SortProperty = "Start", SortDirection = Dto.Enums.SortDirection.Ascending }));
                var personServiceResult = await Fetch(async () => await personApiService.GetAll(new PaginationRequest { Page = 1, PageSize = 1 }, new PersonFilterRequest { EmailAddress = emailAddress}));
               
                if (upcomingActsPagedServiceResult is { Data: not null, Data.Count: > 0 } upcomingActs
                    && updatedActsPagedServiceResult is { Data: not null, Data.Count: > 0 } updatedActs
                    && personServiceResult is { Data: not null, Data.Count: 1 } person)
                {
                    Data = new HomeViewModel
                    {
                        PersonDataResponse = person.Data.First(),
                        UpcomingActDataResponses = upcomingActs.Data,
                        UpdatedActDataResponses = updatedActs.Data
                    };

                    return true;
                }

                return false;
            }

            return await base.HandleCustomRoute(route);
        }


    }
}
