using Microsoft.AspNetCore.Mvc;
using MusicClub.v3.DbCore;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.Api.Controllers.Private
{
    [ApiController]
    [Route("private/[controller]")]
    public class ImageController(IImageDbService imageDbService, MusicClubDbContext dbContext) : Controller
    {
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([/*Min(1), */FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            return Ok(await imageDbService.Get(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest, [FromQuery] ImageFilterRequest filter)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            return Ok(await imageDbService.GetAll(paginationRequest, filter));
        }

        [HttpGet("Download/{id:int}")]
        public async Task<IActionResult> Download(int id)
        {
            var image = await dbContext.Images.FindAsync(id);
            if (image is null)
            {
                return NotFound(); // return serviceResult
            }

            var memoryStream = new MemoryStream(image.Content);

            return File(memoryStream, image.ContentType);
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file, [FromForm] string alt)
        {
            //todo: test
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                // Upload the file if less than 2 MB
                if (memoryStream.Length < 2097152)
                {
                    return Ok(await imageDbService.Create(new ImageDbDataRequest
                    {
                        Alt = alt,
                        Content = memoryStream.ToArray(),
                        ContentType = file.ContentType
                    }));
                }
            }

            //todo: add message file to big
            return ValidationProblem(ModelState);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, IFormFile? file, [FromForm] string alt)
        {
            //todo: test
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            if (file is not null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);

                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        return Ok(await imageDbService.Update(id, new ImageDbDataRequest
                        {
                            Alt = alt,
                            Content = memoryStream.ToArray(),
                            ContentType = file.ContentType
                        }));
                    }
                }
            }
            else
            {
                return Ok(await imageDbService.Update(id, new ImageDbDataRequest
                {
                    Alt = alt,
                    Content = null!, //todo => temp hack!!
                    ContentType = null! //todo => temp hack !!
                }));
            }

            //todo: add message file to big
            return ValidationProblem(ModelState);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([/*Min(1), */FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            return Ok(await imageDbService.Delete(id));
        }
    }
}