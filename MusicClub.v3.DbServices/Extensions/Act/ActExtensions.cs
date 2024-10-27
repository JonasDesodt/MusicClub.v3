using Microsoft.EntityFrameworkCore;
using MusicClub.v3.DbServices.Extensions.GoogleEvent;
using MusicClub.v3.DbServices.Extensions.Image;
using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Enums;
using MusicClub.v3.DbServices.Extensions.Lineup;
using MusicClub.v3.Dto.Filter.Request;

namespace MusicClub.v3.DbServices.Extensions.Act
{
    internal static class ActExtensions
    {
        public static async Task<bool> HasReferenceToLineup(this DbSet<DbCore.Models.Act> acts, int id)
        {
            return await acts.AnyAsync(a => a.LineupId == id);
        }

        public static IQueryable<DbCore.Models.Act> IncludeAll(this IQueryable<DbCore.Models.Act> query)
        {
            return query.Include(x => x.Image)
                        .Include(x => x.Jobs)
                        .Include(x => x.Lineup)
                        .Include(x => x.GoogleEvent)
                        .Include(x => x.Performances);
        }

        public static IQueryable<ActDataResponse> ToResponses(this IQueryable<DbCore.Models.Act> query)
        {
            return query.Select(a => new ActDataResponse
            {
                Description = a.Description,
                Duration = a.Duration,
                Start = a.Start,
                Name = a.Name,
                Title = a.Title,
                PerformancesCount = a.Performances.Count,
                Created = a.Created,
                Id = a.Id,
                ImageDataResponse = a.Image != null ? a.Image.ToResponse() : null,
                Updated = a.Updated,
                JobsCount = a.Jobs.Count,
                LineupDataResponse = a.Lineup!.ToResponse(), //TODO: temp hack (!), deal w/ null reference
                GoogleEventDataResponse = a.GoogleEvent != null ? a.GoogleEvent.ToResponse() : null,
            });
        }

        public static IQueryable<DbCore.Models.Act> Filter(this IQueryable<DbCore.Models.Act> acts, ActFilterRequest filter)
        {
            //todo: description

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                acts = acts.Where(a => a.Name != null && a.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                acts = acts.Where(a => a.Title != null && a.Title.ToLower().Contains(filter.Title.ToLower()));
            }

            if (filter.ImageId > 0)
            {
                acts = acts.Where(a => a.ImageId != null && a.ImageId == filter.ImageId);
            }

            if (filter.LineupId > 0)
            {
                acts = acts.Where(a => a.LineupId == filter.LineupId);
            }

            if (filter.Duration >= 0)
            {
                acts = acts.Where(a => a.Duration == filter.Duration);
            }

            if (filter.Start is { } start)
            {
                acts = acts.Where(a => a.Start != null && a.Start.Value.ToShortDateString().Equals(start.ToShortDateString()));
            }

            if (!string.IsNullOrWhiteSpace(filter.SortProperty))
            {
                if (filter.SortDirection is SortDirection.Descending)
                {
                    acts = filter.SortProperty switch
                    {
                        nameof(DbCore.Models.Act.Name) => acts.OrderByDescending(a => a.Name),
                        nameof(DbCore.Models.Act.Title) => acts.OrderByDescending(a => a.Title),
                        nameof(DbCore.Models.Act.Image) => acts.OrderByDescending(a => a.Image != null ? a.Image.Alt : null),
                        nameof(DbCore.Models.Act.Lineup) => acts.OrderByDescending(a => a.Lineup != null ? a.Lineup.Title : null),
                        nameof(DbCore.Models.Act.Duration) => acts.OrderByDescending(a => a.Duration),
                        nameof(DbCore.Models.Act.Start) => acts.OrderByDescending(a => a.Start),
                        nameof(DbCore.Models.Act.Jobs) => acts.OrderByDescending(a => a.Jobs.Count),
                        nameof(DbCore.Models.Act.Performances) => acts.OrderByDescending(a => a.Performances.Count),
                        nameof(DbCore.Models.Act.Created) => acts.OrderByDescending(a => a.Created),
                        nameof(DbCore.Models.Act.Updated) => acts.OrderByDescending(a => a.Updated),
                        _ => acts.OrderByDescending(a => a.Id),
                    };
                }
                else
                {
                    acts = filter.SortProperty switch
                    {
                        nameof(DbCore.Models.Act.Name) => acts.OrderBy(a => a.Name),
                        nameof(DbCore.Models.Act.Title) => acts.OrderBy(a => a.Title),
                        nameof(DbCore.Models.Act.Image) => acts.OrderBy(a => a.Image != null ? a.Image.Alt : null),
                        nameof(DbCore.Models.Act.Lineup) => acts.OrderBy(a => a.Lineup != null ? a.Lineup.Title : null),
                        nameof(DbCore.Models.Act.Duration) => acts.OrderBy(a => a.Duration),
                        nameof(DbCore.Models.Act.Start) => acts.OrderBy(a => a.Start),
                        nameof(DbCore.Models.Act.Jobs) => acts.OrderBy(a => a.Jobs.Count),
                        nameof(DbCore.Models.Act.Performances) => acts.OrderBy(a => a.Performances.Count),
                        nameof(DbCore.Models.Act.Created) => acts.OrderBy(a => a.Created),
                        nameof(DbCore.Models.Act.Updated) => acts.OrderBy(a => a.Updated),
                        _ => acts.OrderBy(a => a.Id),
                    };
                }
            }

            return acts;
        }


        public static DbCore.Models.Act Update(this DbCore.Models.Act act, ActDataRequest actDataRequest)
        {
            act.Name = actDataRequest.Name;
            act.Title = actDataRequest.Title;
            act.ImageId = actDataRequest.ImageId;
            act.Updated = DateTime.UtcNow;
            act.Duration = actDataRequest.Duration;
            act.Start = actDataRequest.Start;
            act.LineupId = actDataRequest.LineupId;
            act.Description = actDataRequest.Description;

            return act;
        }

        public static ActDataResponse ToResponse(this DbCore.Models.Act act)
        {
            return new ActDataResponse
            {
                Name = act.Name,
                Title = act.Title,
                Description = act.Description,
                PerformancesCount = act.Performances.Count,
                Created = act.Created,
                Id = act.Id,
                ImageDataResponse = act.Image != null ? act.Image.ToResponse() : null,
                Updated = act.Updated,
                JobsCount = act.Jobs.Count,
                LineupDataResponse = act.Lineup!.ToResponse(), //TODO: temp hack (!), deal w/ null reference
                Duration = act.Duration,
                Start = act.Start,
                GoogleEventDataResponse = act.GoogleEvent != null ? act.GoogleEvent.ToResponse() : null
            };
        }
    }
}
