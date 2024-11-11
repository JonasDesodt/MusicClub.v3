using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicClub.v3.Dto.Statistics.Requests;
using MusicClub.v3.Dto.Statistics.Responses;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.Api.Controllers.Private
{
    [Authorize]
    [ApiController]
    [Route("private/[controller]")]
    public class StatisticsController : ControllerBase
    {
        [HttpGet("attendance-per-month")]
        public async Task<IActionResult> AttendancePerMonth([FromQuery] AttendancePerMonthStatisticsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new ServiceResult<AttendancePerMonthStatisticsResponse>
            {
                Data = new AttendancePerMonthStatisticsResponse
                {
                    From = request.From,
                    Until = request.Until,
                    Statistics = new Dictionary<int, int>
                    {
                        { 0, 75 },
                        { 1, 77 },
                        { 2, 74 },
                        { 3, 72 },
                        { 4, 80 },
                        { 5 , 59 },
                        { 6, 68 },
                        { 7, 74 },
                        { 8, 67 },
                        { 9, 71 },
                        { 10, 72 },
                        { 11, 79 }
                    }

                }
            });
        }
    }
}
