using System.ComponentModel.DataAnnotations;
//using MusicClub.v3.Dto.Attributes;

namespace MusicClub.v3.Dto.Transfer
{
    public class PaginationRequest
    {
        [Required]
        //[Min(1)]
        public required int Page { get; set; }

        [Required]
        //[Between(1, 24)]
        public required int PageSize { get; set; }
    }
}