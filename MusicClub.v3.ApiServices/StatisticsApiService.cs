using MusicClub.v3.ApiServices.Extensions;
using MusicClub.v3.Dto.Statistics.Requests;
using MusicClub.v3.Dto.Statistics.Responses;
using MusicClub.v3.Dto.Transfer;
using System.Net;
using System.Net.Http.Json;

namespace MusicClub.v3.ApiServices
{
    public class StatisticsApiService(IHttpClientFactory httpClientFactory)
    {
        public async Task<ServiceResult<AttendancePerMonthStatisticsResponse>> GetAttendancePerMonth(AttendancePerMonthStatisticsRequest request)
        {
            var httpClient = httpClientFactory.CreateClient("MusicClubApi");

            var responseMessage = await httpClient.GetAsync($"private/statistics/attendance-per-month?from={request.From}&until={request.Until}");

            responseMessage.EnsureSuccessStatusCode();

            var serviceResult = await responseMessage.Content.ReadFromJsonAsync<ServiceResult<AttendancePerMonthStatisticsResponse>>();

            if(serviceResult is null)
            {
                return new ServiceResult<AttendancePerMonthStatisticsResponse>();
            }

            return serviceResult;            
        }
    }
}
