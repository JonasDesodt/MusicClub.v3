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

namespace MusicClub.v3.Cms.Controllers
{
    [GenerateDataController]
    public partial class DataController(NavigationManager navigationManager, MemoryService memoryService, AuthenticationStateProvider authenticationStateProvider, IActService actApiService)/*, IArtistService artistApiService, IPersonService personApiService, IPerformanceService performanceApiService, IImageApiService imageApiService, ILineupService lineupApiService)*/ : DataControllerBase(navigationManager, memoryService, authenticationStateProvider)
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
                Data = await Fetch(async () => await _actApiService.GetAll(new PaginationRequest { Page = 1, PageSize = 12 }, new ActFilterRequest { }));

                if (Data is PagedServiceResult<IList<ActDataResponse>, ActFilterResponse> pagedServiceResult)
                {
                    return pagedServiceResult.Messages?.HasMessage is not true;
                }

                return false;
            }

            return await base.HandleCustomRoute(route);
        }


    }
}
