
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text;

namespace Curso.ComercioElectronico.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        private readonly JwtConfiguration jwtConfiguration;

        public TokenController(IOptions<JwtConfiguration> options)
        {
            this.jwtConfiguration = options.Value;
        }


        [HttpPost]
        public async Task<string> TokenAsync(UserInput input)
        {

            //1. Validar User.
            var userTest = "foo";
            if (input.UserName != userTest || input.Password != "123")
            {
                throw new AuthenticationException("User or Passowrd incorrect!");
            }
            

            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, userTest),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserName", userTest),
                       
                    };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(
                jwtConfiguration.Issuer,
                jwtConfiguration.Audience,
                claims,
                expires: DateTime.UtcNow.Add(jwtConfiguration.Expires),
                signingCredentials: signIn);


            var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);


            return jwt;
        }
    }

    public class UserInput
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }



}
