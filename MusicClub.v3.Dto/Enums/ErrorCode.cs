using System.Net;

namespace MusicClub.v3.Dto.Enums
{
    public enum ErrorCode
    { 
        //Database errors
        NotFound = 600,
        Referenced = 601,
        NotCreated = 602,
        NotDeleted = 603,
        NotUpdated = 604,
        Duplicate = 605,

        //API errors
        FetchError = 701
    }
}
