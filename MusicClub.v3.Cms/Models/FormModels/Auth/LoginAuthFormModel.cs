using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MusicClub.v3.Cms.Models.FormModels.Auth
{
    public class LoginAuthFormModel
    {
        [Required]
        [EmailAddress]
        public string? Emailaddress { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
