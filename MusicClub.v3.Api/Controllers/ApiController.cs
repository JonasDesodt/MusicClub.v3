using Microsoft.AspNetCore.Mvc;
using MusicClub.v3.Api.SourceGeneratorAttributes;

namespace MusicClub.v3.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [GenerateControllers("Artist", "Person")]
    public class ApiController<TDataRequest, TDataResponse, TFilterRequest, TFilterResponse>(IService<TDataRequest, TDataResponse, TFilterRequest, TFilterResponse> dbService) : ControllerBase
    {
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            return Ok();    
        }
    }
}