using Demarco.Application;
using Demarco.Application.Interfaces;
using Demarco.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Demarco.API.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ILoginApp _loginApp;

        public LoginController(IConfiguration configuration,
                               ILoginApp loginApp)
        {
            var jwtSection = configuration.GetSection("JwtSettings");
            _jwtSettings =  jwtSection.Get<JwtSettings>();

            _loginApp = loginApp;
        }

        [HttpPost]
        public IActionResult Post([FromBody] LoginDTO loginDTO)
        {
            var token = _loginApp.Logar(loginDTO, _jwtSettings);

            if (!string.IsNullOrWhiteSpace(token))
                return Ok(token);

            return Unauthorized();
        }
    }
}
