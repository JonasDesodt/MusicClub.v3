using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MusicClub.v3.DbCore.Models;
using MusicClub.v3.Dto.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MusicClub.v3.Api.Controllers.Private
{
    [Route("api/[controller]")]
    public class AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration) : ControllerBase
    {
        [HttpPost("token")]
        public async Task<IActionResult> Token(TokenAuthRequest tokenAuthRequest)//[FromForm] string username, [FromForm] string password)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized();
            }

            var user = await userManager.FindByNameAsync(tokenAuthRequest.Emailaddress);//username);
            if (user == null || !await userManager.CheckPasswordAsync(user, tokenAuthRequest.Password))// password))
            {
                return Unauthorized();
            }

            var token = GenerateJwtToken(user);

            return Ok(new { token });
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? throw new NullReferenceException("The IdentityUser has no username.")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"] ?? throw new InvalidOperationException("JWT Secret is missing in configuration.")));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
