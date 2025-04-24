using Demarco.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demarco.Application.Interfaces
{
    public interface IEmpregadoApp
    {
        Task<bool> Incluir(EmpregadoDTO empregadoDTO);

        Task<bool> Atualizar(EmpregadoDTO empregadoDTO);

        IEnumerable<EmpregadoDTO> RecuperarTodos();

        EmpregadoDTO Recuperar(int id);
    }
}
