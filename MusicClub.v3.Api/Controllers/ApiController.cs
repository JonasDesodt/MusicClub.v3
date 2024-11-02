using Microsoft.AspNetCore.Mvc;
using MusicClub.v3.Api.SourceGeneratorAttributes;
using MusicClub.v3.DbCore.Models;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [GenerateControllers(
        nameof(Act), 
        nameof(Artist), 
        nameof(Band), 
        nameof(Bandname), 
        nameof(Function),
        nameof(GoogleCalendar),
        nameof(GoogleEvent),
        nameof(Job),
        nameof(Lineup),
        nameof(Performance), 
        nameof(Person),
        nameof(Service),
        nameof(Worker))]
    public class ApiController<TDataRequest, TDataResponse, TFilterRequest, TFilterResponse>(IService<TDataRequest, TDataResponse, TFilterRequest, TFilterResponse> dbService) : ControllerBase
    {
        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] TDataRequest actRequest)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            return Ok(await dbService.Create(actRequest));
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest, [FromQuery] TFilterRequest filter)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            return Ok(await dbService.GetAll(paginationRequest, filter));
        }

        [HttpGet("{id:int}")]
        public virtual async Task<IActionResult> Get([/*Min(1), */FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            return Ok(await dbService.Get(id));
        }

        [HttpPut("{id:int}")]
        public virtual async Task<IActionResult> Update([/*Min(1), */FromRoute] int id, [FromBody] TDataRequest personRequest)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            return Ok(await dbService.Update(id, personRequest));
        }

        [HttpDelete("{id:int}")]
        public virtual async Task<IActionResult> Delete([/*Min(1), */FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            return Ok(await dbService.Delete(id));
        }
    }
}