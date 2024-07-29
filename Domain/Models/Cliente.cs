using Domain.Dto;
using Domain.Enumerations;
using System.Collections.ObjectModel;

namespace Domain.Models
{
    public class Cliente
    {
        public string Id { get; set; }
        public string Cpf { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public string Sexo { get; set; }

        public Cliente()
        {
        }

        public Cliente(ClienteDto dto)
        {
            Id = Ulid.NewUlid().ToString();
            Cpf = dto.cpf;
            Usuario = dto.usuario; 
            Senha = dto.senha; 
            Nome = dto.nome; 
            Celular = dto.celular;
            Email = dto.email;
            Endereco = dto.endereco; 
            Sexo = dto.sexo;
        }
    }
}
