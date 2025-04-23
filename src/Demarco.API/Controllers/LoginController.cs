using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Demarco.DTOs;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Demarco.API.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly JwtSettings _jwtSettings;

        public LoginController(IConfiguration configuration)
        {
            var jwtSection = configuration.GetSection("JwtSettings");
            _jwtSettings =  jwtSection.Get<JwtSettings>();
        }

        [HttpPost]
        public IActionResult Post([FromBody] LoginDto login)
        {
            if (login.Usuario == "admin" && login.Senha == "123")
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, login.Usuario)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = _jwtSettings.Issuer,
                    Audience = _jwtSettings.Audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(tokenHandler.WriteToken(token));
            }

            return Unauthorized();
        }
    }
}
