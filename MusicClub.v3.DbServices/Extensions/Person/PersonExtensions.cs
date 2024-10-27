using Microsoft.EntityFrameworkCore;
using MusicClub.v3.DbServices.Extensions.Image;
using MusicClub.v3.DbServices.Extensions.Person;
using MusicClub.v3.Dto.Data.Request;
using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Enums;
using MusicClub.v3.Dto.Filter.Request;

namespace MusicClub.v3.DbServices.Extensions.Person
{
    internal static class PersonExtensions
    {
        public static PersonDataResponse ToResponse(this DbCore.Models.Person person)
        {
            return new PersonDataResponse
            {
                ApplicationUsersCount = person.ApplicationUsers.Count,
                ArtistsCount = person.Artists.Count,
                Created = person.Created,
                Firstname = person.Firstname,
                Id = person.Id,
                ImageDataResponse = person.Image?.ToResponse(),
                Lastname = person.Lastname,
                Updated = person.Updated,
                WorkersCount = person.Workers.Count,
            };
        }

        public static IQueryable<PersonDataResponse> ToResponses(this IQueryable<DbCore.Models.Person> query)
        {
            return query.Select(p => new PersonDataResponse
            {
                ApplicationUsersCount = p.ApplicationUsers.Count,
                ArtistsCount = p.Artists.Count,
                Created = p.Created,
                Firstname = p.Firstname,
                Id = p.Id,
                ImageDataResponse = p.Image != null ? p.Image.ToResponse() : null,
                Lastname = p.Lastname,
                Updated = p.Updated,
                WorkersCount = p.Workers.Count,
            });
        }

        public static IQueryable<DbCore.Models.Person> IncludeAll(this IQueryable<DbCore.Models.Person> query)
        {
            return query.Include(p => p.Image)
                        .Include(p => p.ApplicationUsers)
                        .Include(p => p.Artists)
                        .Include(p => p.Workers);
        }

        public static DbCore.Models.Person Update(this DbCore.Models.Person person, PersonDataRequest personRequest)
        {
            person.Firstname = personRequest.Firstname;
            person.Lastname = personRequest.Lastname;
            person.ImageId = personRequest.ImageId;
            person.Updated = DateTime.UtcNow;

            return person;
        }

        public static IQueryable<DbCore.Models.Person> Filter(this IQueryable<DbCore.Models.Person> people, PersonFilterRequest filterRequest)
        {
            if (!string.IsNullOrWhiteSpace(filterRequest.Firstname))
            {
                people = people.Where(a => a.Firstname != null && a.Firstname.ToLower().Contains(filterRequest.Firstname.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filterRequest.Lastname))
            {
                people = people.Where(a => a.Lastname != null && a.Lastname.ToLower().Contains(filterRequest.Lastname.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filterRequest.SortProperty))
            {
                if (filterRequest.SortDirection is SortDirection.Descending)
                {
                    people = filterRequest.SortProperty switch
                    {
                        nameof(PersonDataResponse.Firstname) => people.OrderByDescending(a => a.Firstname),
                        nameof(PersonDataResponse.Lastname) => people.OrderByDescending(a => a.Lastname),
                        _ => people.OrderByDescending(a => a.Id),
                    };
                }
                else
                {
                    people = filterRequest.SortProperty switch
                    {
                        nameof(PersonDataResponse.Firstname) => people.OrderBy(a => a.Firstname),
                        nameof(PersonDataResponse.Lastname) => people.OrderBy(a => a.Lastname),
                        _ => people.OrderBy(a => a.Id),
                    };
                }
            }

            return people;
        }
    }
}