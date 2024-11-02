using Microsoft.EntityFrameworkCore;
using MusicClub.v3.Dto.Enums;
using MusicClub.v3.Dto.Filter.Request;
using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.DbServices.Extensions.Lineup;
using MusicClub.v3.DbServices.Extensions.Image;

namespace MusicClub.v3.DbServices.Extensions.Lineup
{
    internal static class LineupExtensions
    {
        public static LineupDataResponse ToResponse(this DbCore.Models.Lineup lineup)
        {
            return new LineupDataResponse
            {
                ActsCount = lineup.Acts.Count,
                Created = lineup.Created,
                Doors = lineup.Doors,
                Id = lineup.Id,
                ServicesCount = lineup.Services.Count,
                Updated = lineup.Updated,
                Title = lineup.Title,
                ImageDataResponse = lineup.Image?.ToResponse()
            };
        }

        public static IQueryable<DbCore.Models.Lineup> IncludeAll(this IQueryable<DbCore.Models.Lineup> query)
        {
            return query.Include(a => a.Image)
                        .Include(a => a.Services)
                        .Include(a => a.Acts);
        }

        public static IQueryable<LineupDataResponse> ToResponses(this IQueryable<DbCore.Models.Lineup> query)
        {
            return query.Select(a => new LineupDataResponse
            {
                ActsCount = a.Acts.Count,
                Created = a.Created,
                Doors = a.Doors,
                Id = a.Id,
                ServicesCount = a.Services.Count,
                Updated = a.Updated,
                Title = a.Title,
                ImageDataResponse = a.Image != null ? a.Image.ToResponse() : null
            });
        }

        public static IQueryable<DbCore.Models.Lineup> Filter(this IQueryable<DbCore.Models.Lineup> lineups, LineupFilterRequest filterRequest)
        {
            if (!string.IsNullOrWhiteSpace(filterRequest.Title))
            {
                lineups = lineups.Where(l => l.Title != null && l.Title.ToLower().Contains(filterRequest.Title.ToLower()));
            } // note => if both title & deepSearch are set, title will be used!
            else if (!string.IsNullOrWhiteSpace(filterRequest.DeepSearch))
            {
                lineups = lineups
                    .Where(l =>
                            l.Title != null && l.Title.ToLower().Contains(filterRequest.DeepSearch.ToLower())
                            || l.Acts.Any(a => a.Name.ToLower().Contains(filterRequest.DeepSearch.ToLower()) || a.Title != null && a.Title.ToLower().Contains(filterRequest.DeepSearch.ToLower()) /*|| a.Description != null && a.Description.ToLower().Contains(filterRequest.DeepSearch.ToLower())*/));
                       
            }

            if(filterRequest.Between is { } between)
            {
                if(between.From is { } from)
                {
                    lineups = lineups.Where(l => between.IncludeFrom ? l.Doors >= between.From : l.Doors > between.From);
                }

                if(between.To is { } to)
                {
                    lineups = lineups.Where(l => between.IncludeTo ? l.Doors <= between.To : l.Doors < between.To);
                }
            }

            if (filterRequest.ImageId > 0)
            {
                lineups = lineups.Where(l => l.ImageId != null && l.ImageId == filterRequest.ImageId);
            }

            if (filterRequest.Doors is { } doors)
            {
                lineups = lineups.Where(l => l.Doors.ToShortDateString().Equals(doors.ToShortDateString()));
            }

            if (!string.IsNullOrWhiteSpace(filterRequest.SortProperty))
            {
                if (filterRequest.SortDirection is SortDirection.Descending)
                {
                    lineups = filterRequest.SortProperty switch
                    {
                        nameof(DbCore.Models.Lineup.Title) => lineups.OrderByDescending(l => l.Title),
                        nameof(DbCore.Models.Lineup.Image) => lineups.OrderByDescending(l => l.Image != null ? l.Image.Alt : null),
                        nameof(DbCore.Models.Lineup.Doors) => lineups.OrderByDescending(l => l.Doors),
                        nameof(DbCore.Models.Lineup.Created) => lineups.OrderByDescending(l => l.Created),
                        nameof(DbCore.Models.Lineup.Updated) => lineups.OrderByDescending(l => l.Updated),
                        nameof(DbCore.Models.Lineup.Services) => lineups.OrderByDescending(l => l.Services.Count),
                        nameof(DbCore.Models.Lineup.Acts) => lineups.OrderByDescending(l => l.Acts.Count),
                        _ => lineups.OrderByDescending(l => l.Id),
                    };
                }
                else
                {
                    lineups = filterRequest.SortProperty switch
                    {
                        nameof(DbCore.Models.Lineup.Title) => lineups.OrderBy(l => l.Title),
                        nameof(DbCore.Models.Lineup.Image) => lineups.OrderBy(l => l.Image != null ? l.Image.Alt : null),
                        nameof(DbCore.Models.Lineup.Doors) => lineups.OrderBy(l => l.Doors),
                        nameof(DbCore.Models.Lineup.Created) => lineups.OrderBy(l => l.Created),
                        nameof(DbCore.Models.Lineup.Updated) => lineups.OrderBy(l => l.Updated),
                        nameof(DbCore.Models.Lineup.Services) => lineups.OrderBy(l => l.Services.Count),
                        nameof(DbCore.Models.Lineup.Acts) => lineups.OrderBy(l => l.Acts.Count),
                        _ => lineups.OrderBy(l => l.Id),
                    };
                }
            }

            return lineups;
        }

        public static DbCore.Models.Lineup Update(this DbCore.Models.Lineup lineup, LineupDataRequest dataRequest)
        {
            lineup.ImageId = dataRequest.ImageId;
            lineup.Updated = DateTime.UtcNow;
            lineup.Title = dataRequest.Title;
            lineup.Doors = dataRequest.Doors;

            return lineup;
        }
    }
}
