using System;
using System.Collections.Generic;
using System.Text;

namespace Demarco.Domain
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; private set; }
        public string Senha { get; private set; }

        public Usuario(int id, string nome, string senha)
        {
            ID = id;
            Nome = nome;
            Senha = senha;
        }

        public Usuario() { }
    }
}
