using Microsoft.AspNetCore.Mvc;
using MusicClub.v3.Api.ActionAttributes;
using MusicClub.v3.DbCore.Models;
using MusicClub.v3.Dto.Public;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.Api.Controllers.Public
{
    [ApiKey]
    [ApiController]
    [Route("public/[controller]")]
    public class AgendaController(ILineupService lineupDbService, IActService actDbService, ILanguageService languageDbService, IDescriptionTranslationService descriptionTranslationDbService) : ControllerBase
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

        [HttpGet("{locale}/{id:int}")]
        public async Task<IActionResult> Detail([FromRoute] string locale, [FromRoute] int id)
        {
            var lineupServiceResult = await lineupDbService.Get(id);
            if (lineupServiceResult.Data is not { } lineupDataResponse)
            {
                return BadRequest();
            }

            var actPagedServiceResult = await actDbService.GetAll(new PaginationRequest
            {
                Page = 1,
                PageSize = 24
            },
            new ActFilterRequest
            {
                LineupId = lineupDataResponse.Id,
                SortProperty = "Start",
                SortDirection = Dto.Enums.SortDirection.Descending
            });

            if (actPagedServiceResult.Data is not { } actDataResponses)
            {
                return BadRequest();
            }

            var pages = (int)Math.Ceiling((double)actPagedServiceResult.PaginationResponse.TotalCount / actPagedServiceResult.PaginationResponse.PageSize);


            for (var i = 2; i < 2 + pages; i++)
            {
                var addActPagedServiceResult = await actDbService.GetAll(new PaginationRequest
                {
                    Page = i,
                    PageSize = 24,
                },
                new ActFilterRequest
                {
                    LineupId = lineupDataResponse.Id,
                    SortProperty = "Start",
                    SortDirection = Dto.Enums.SortDirection.Descending
                });

                if (addActPagedServiceResult.Data is not { } addActDataResponses)
                {
                    return BadRequest();
                }

                actDataResponses = [.. actDataResponses, .. addActDataResponses];
            }

            var languagePagedServiceResult = await languageDbService.GetAll(new PaginationRequest
            {
                Page = 1,
                PageSize = 1,
            },
            new LanguageFilterRequest
            {
                Identifier = locale,
            });

            if (languagePagedServiceResult.Data is not { Count: > 0 } languageDataResponses)
            {
                return BadRequest();
            }

            var actsTotalCount = actDataResponses.Count;

            var lineupPublicResponse = new LineupPublicResponse
            {
                Doors = lineupDataResponse.Doors,
                Id = lineupDataResponse.Id,
                Title = lineupDataResponse.Title,
                Image = lineupDataResponse.ImageDataResponse != null
                    ? new ImagePublicResponse
                    {
                        Alt = lineupDataResponse.ImageDataResponse.Alt,
                        Id = lineupDataResponse.ImageDataResponse.Id
                    }
                    : null,
                ActsPage = 1,
                ActsPageSize = actsTotalCount,
                ActsTotalCount = actsTotalCount
            };

            foreach (var actDataResponse in actDataResponses)
            {
                var actPublicResponse = new ActPublicResponse
                {
                    Id = actDataResponse.Id,
                    Name = actDataResponse.Name,
                    Title = actDataResponse.Title,
                    Duration = actDataResponse.Duration,
                    Start = actDataResponse.Start,
                    Image = actDataResponse.ImageDataResponse != null
                    ? new ImagePublicResponse
                    {
                        Alt = actDataResponse.ImageDataResponse.Alt,
                        Id = actDataResponse.ImageDataResponse.Id
                    }
                    : null
                };

                if (actDataResponse.DescriptionDataResponse is { } descriptionDataResponse)
                {
                    var descriptionTranslationPagedServiceResult = await descriptionTranslationDbService.GetAll(new PaginationRequest
                    {
                        Page = 1,
                        PageSize = 1
                    },
                    new DescriptionTranslationFilterRequest
                    {
                        DescriptionId = descriptionDataResponse.Id,
                        LanguageId = languageDataResponses[0].Id
                    });

                    if (descriptionTranslationPagedServiceResult.Data is not { Count: > 0 } descriptionTranslationDataResponses)
                    {
                        return BadRequest();
                    }

                    actPublicResponse.Description = descriptionTranslationPagedServiceResult.Data[0].Text;
                }

                lineupPublicResponse.Acts.Add(actPublicResponse);
            }

            return Ok(lineupPublicResponse);
        }
    }
}
