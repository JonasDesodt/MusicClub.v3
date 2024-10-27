using Microsoft.AspNetCore.Components.Forms;
using MusicClub.v3.Dto.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MusicClub.v3.Dto.Data.Request
{
    public class ImageApiDataRequest
    {
        [Required] // todo: test
        public required string Alt { get; set; }

        [Required] // todo: test
        [MaxFileSize] // todo: test
        public required IBrowserFile? File { get; set; }
    }
}
