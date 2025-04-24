using Demarco.Application.Interfaces;
using Demarco.Domain;
using Demarco.DTOs;
using Demarco.Repository.Interface;
using System;
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
            ValidarCpf(empregadoDTO.CPF);
            ValidarCpfJaExiste(empregadoDTO.CPF, empregadoDTO.ID);
            ValidarDataNascimento(empregadoDTO.DataNascimento);

            Empregado emp = new Empregado(empregadoDTO.ID, empregadoDTO.CPF, empregadoDTO.Nome, empregadoDTO.DataNascimento);
            _empregadoRepository.Add(emp);
            return await _empregadoRepository.SaveChangesAsync();
        }

        public async Task<bool> Atualizar(EmpregadoDTO empregadoDTO)
        {
            ValidarCpf(empregadoDTO.CPF);
            ValidarCpfJaExiste(empregadoDTO.CPF, empregadoDTO.ID);
            ValidarDataNascimento(empregadoDTO.DataNascimento);

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

        private void ValidarDataNascimento(DateTime dataNascimento)
        {
            var idade = DateTime.Today.Year - dataNascimento.Year;
            if (dataNascimento.Date > DateTime.Today.AddYears(-idade))
            {
                idade--;
            }

            if (idade < 18)
            {
                throw new ArgumentException("O empregado deve ter pelo menos 18 anos.");
            }
        }

        private void ValidarCpf(string cpf)
        {
            if (cpf.Distinct().Count() == 1)
            {
                throw new ArgumentException("O CPF informado é inválido.");
            }

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            tempCpf += digito1;
            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            string cpfCalculado = tempCpf + digito2;

            if (cpf != cpfCalculado)
            {
                throw new ArgumentException("O CPF informado é inválido.");
            }
        }

        private void ValidarCpfJaExiste(string cpf, int id)
        {
            if(_empregadoRepository.GetAsync(c => c.CPF == cpf && c.ID != id).Result.Count() > 0)
            {
                throw new ArgumentException("CPF já cadastrado.");
            }
        }
    }
}
