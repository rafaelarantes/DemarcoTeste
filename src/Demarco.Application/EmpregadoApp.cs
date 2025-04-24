using Demarco.Application.Interfaces;
using Demarco.Domain;
using Demarco.DTOs;
using Demarco.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demarco.Application
{
    public class EmpregadoApp : IEmpregadoApp
    {
        private readonly IEmpregadoRepository _empregadoRepository;

        public EmpregadoApp(IEmpregadoRepository repository)
        {
            this._empregadoRepository = repository;
        }

        public async Task<bool> Incluir(EmpregadoDTO empregadoDTO)
        {
            Empregado emp = new Empregado(empregadoDTO.ID, empregadoDTO.CPF, empregadoDTO.Nome, empregadoDTO.DataNascimento);
            _empregadoRepository.Add(emp);
            return await _empregadoRepository.SaveChangesAsync();
        }

        public async Task<bool> Atualizar(EmpregadoDTO empregadoDTO)
        {
            var empregado = await _empregadoRepository.GetAsync(empregadoDTO.ID);

            if (empregado == null)
            {
                return await Task.FromResult(false);
            }

            empregado.AtualizarCPF(empregadoDTO.CPF);
            empregado.AtualizarNome(empregadoDTO.Nome);
            empregado.AtualizarDataNascimento(empregadoDTO.DataNascimento); 

            _empregadoRepository.UpDate(empregado);

            return await _empregadoRepository.SaveChangesAsync();
        }

        public IEnumerable<EmpregadoDTO> RecuperarTodos()
        {
            return _empregadoRepository.GetAsync().Result.Select(e => new EmpregadoDTO
            {
                ID = e.ID,
                CPF = e.CPF,
                Nome = e.Nome,
                DataNascimento = e.DataNascimento
            });
        }

        public EmpregadoDTO Recuperar(int id)
        {
            var empregado = _empregadoRepository.GetAsync(id).Result;
            if (empregado != null)
            {
                return new EmpregadoDTO
                {
                    ID = empregado.ID,
                    CPF = empregado.CPF,
                    Nome = empregado.Nome,
                    DataNascimento = empregado.DataNascimento
                };
            }
            return null;
        }
    }
}
