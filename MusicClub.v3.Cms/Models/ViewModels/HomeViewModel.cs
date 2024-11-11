using MusicClub.v3.Dto.Data.Response;
using MusicClub.v3.Dto.Statistics.Responses;

namespace MusicClub.v3.Cms.Models.ViewModels
{
    public class HomeViewModel
    {
        public required PersonDataResponse PersonDataResponse { get; set; }

        public IList<ActDataResponse> UpcomingActDataResponses { get; set; } = [];

        public IList<ActDataResponse> UpdatedActDataResponses { get; set; } = [];

        public required AttendancePerMonthStatisticsResponse AttendancePerMonthStatisticsResponse { get;set;}
    }
}
