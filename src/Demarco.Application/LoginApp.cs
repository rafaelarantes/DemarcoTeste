using Demarco.Application.Interfaces;
using Demarco.DTOs;
using Demarco.Repository.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Demarco.Application
{
    public class LoginApp : ILoginApp
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginApp(IUsuarioRepository usuarioRepository)
        {
            this._usuarioRepository = usuarioRepository;
        }

        public string Logar(LoginDTO loginDto, JwtSettings jwtSettings)
        {
            var senha = GerarSHA256(loginDto.Senha);

            var usuario = _usuarioRepository.GetAsync(x => x.Nome == loginDto.Usuario 
                                                        && x.Senha == senha).Result.FirstOrDefault();

            if(usuario != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, loginDto.Usuario)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = jwtSettings.Issuer,
                    Audience = jwtSettings.Audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }

            return string.Empty;
        }

        private string GerarSHA256(string texto)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(texto);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}
