using Domain.Dto;
using Domain.Enumerations;

namespace WebAPI.Controllers.UseCases.Clientes.AdicionarClientes
{
    public class AdicionarClientesRequest
    {
        public string Cpf { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public string Sexo { get; set; }
    }
}
