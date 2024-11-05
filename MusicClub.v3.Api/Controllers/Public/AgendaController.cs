using Microsoft.AspNetCore.Mvc;
using MusicClub.v3.Api.ActionAttributes;
using MusicClub.v3.Dto.Public;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.Api.Controllers.Public
{
    [ApiKey]
    [ApiController]
    [Route("public/[controller]")]
    public class AgendaController(ILineupService lineupDbService, IActService actDbService, IDescriptionTranslationService descriptionTranslationService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] PaginationRequest paginationRequest, [FromQuery] string? search = null, [FromQuery] DateTime? from = null, [FromQuery] DateTime? until = null)
        {
            var lineupPagedServiceResult = await lineupDbService.GetAll(paginationRequest, new LineupFilterRequest
            {
                Between = new Between<DateTime>
                {
                    From = from,
                    To = until
                },
                DeepSearch = search
            });
            if (lineupPagedServiceResult.Data is not { } lineupDataResponses)
            {
                return BadRequest();
            }

            var agendaIndexPublicResponse = new AgendaIndexPublicResponse
            {
                Page = lineupPagedServiceResult.PaginationResponse.Page,
                PageSize = lineupPagedServiceResult.PaginationResponse.PageSize,
                TotalCount = lineupPagedServiceResult.PaginationResponse.TotalCount,
                From = from,
                Until = until,
                Search = search
            };

            foreach (var lineupDataResponse in lineupDataResponses)
            {
                var actPagedServiceResultPaginationRequest = new PaginationRequest
                {
                    Page = 1,
                    PageSize = 5
                };

                var actPagedServiceResultActFilterRequest = new ActFilterRequest
                {
                    LineupId = lineupDataResponse.Id,
                    SortDirection = Dto.Enums.SortDirection.Descending,
                    SortProperty = "Start"
                };

                var actPagedServiceResult = await actDbService.GetAll(actPagedServiceResultPaginationRequest, actPagedServiceResultActFilterRequest);
                if (actPagedServiceResult.Data is not { } actDataResponses)
                {
                    return BadRequest();
                }

                agendaIndexPublicResponse.Lineups.Add(new LineupPublicResponse
                {
                    Id = lineupDataResponse.Id,
                    Title = lineupDataResponse.Title,
                    Doors = lineupDataResponse.Doors,
                    ActsPage = actPagedServiceResult.PaginationResponse.Page,
                    ActsPageSize = actPagedServiceResult.PaginationResponse.PageSize,
                    ActsTotalCount = actPagedServiceResult.PaginationResponse.TotalCount,
                    Image = lineupDataResponse.ImageDataResponse != null
                    ? new ImagePublicResponse
                    {
                        Alt = lineupDataResponse.ImageDataResponse.Alt,
                        Id = lineupDataResponse.ImageDataResponse.Id
                    }
                    : null,
                    Acts = actDataResponses.Select(a => new ActPublicResponse
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Title = a.Title,
                        Duration = a.Duration,
                        Start = a.Start,
                        Image = a.ImageDataResponse != null
                        ? new ImagePublicResponse
                        {
                            Alt = a.ImageDataResponse.Alt,
                            Id = a.ImageDataResponse.Id
                        }
                        : null
                    }).ToList()
                });

            }

            return Ok(agendaIndexPublicResponse);
        }

        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> Detail()
        //{
        //    return Ok();
        //}
    }
}
