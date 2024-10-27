using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.DbCore.Models;

namespace MusicClub.v3.DbServices.Extensions.Band
{
    public static class BandExtensions
    {
        public static BandDataResponse ToResponse(this DbCore.Models.Band band)
        {
            return new BandDataResponse
            {
                BandnamesCount = band.Bandnames.Count,
                Created = band.Created,
                Id = band.Id,
                Updated = band.Updated
            };
        }
    }
}
