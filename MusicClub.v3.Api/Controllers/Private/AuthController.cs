using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MusicClub.v3.DbCore;
using MusicClub.v3.DbCore.Models;
using MusicClub.v3.Dto.Auth.Request;
using MusicClub.v3.Dto.Auth.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MusicClub.v3.Api.Controllers.Private
{
    [Route("private/[controller]")]
    public class AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration, MusicClubDbContext dbContext) : ControllerBase
    {
        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody] TokenAuthRequest tokenAuthRequest)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized();
            }

            var user = await userManager.FindByNameAsync(tokenAuthRequest.Emailaddress);
            if (user == null || !await userManager.CheckPasswordAsync(user, tokenAuthRequest.Password))
            {
                return Unauthorized();
            }

            var token = GenerateJwtToken(user);

            return Ok(new TokenAuthResponse { 
                AccessToken = token,
                ExpiresIn = 60 * 60, //todo => get from jwt settings
                TokenType = "Bearer"
            });
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            //throws exception & returns internal server error when there is no single person found for the user, because this should never happen
            //todo => this needs more testing
            var person = dbContext.People.Include(p => p.ApplicationUsers).Single(p => p.ApplicationUsers.Any(a => a.Id == user.Id));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? throw new NullReferenceException("The IdentityUser has no username.")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, person.Firstname) // todo => use ClaimTypes.GivenName?
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"] ?? throw new InvalidOperationException("JWT Secret is missing in configuration.")));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
