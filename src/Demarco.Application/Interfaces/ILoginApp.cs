using Demarco.DTOs;
using System.Threading.Tasks;

namespace Demarco.Application.Interfaces
{
    public interface ILoginApp
    {
        string Logar(LoginDTO loginDto, JwtSettings jwtSettings);
    }
}
