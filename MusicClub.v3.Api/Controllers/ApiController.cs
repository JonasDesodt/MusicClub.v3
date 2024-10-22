using Microsoft.AspNetCore.Mvc;
using MusicClub.v3.Api.Attributes;

namespace MusicClub.v3.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [GenerateControllers("Act", "Artist", "GoogleCalendar", "GoogleEvent", "Lineup", "Performance", "Person")]
    public class ApiController : ControllerBase
    {

    }
}