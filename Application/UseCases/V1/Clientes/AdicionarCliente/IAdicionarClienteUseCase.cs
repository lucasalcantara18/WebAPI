using Domain.Dto;
using System.Threading.Tasks;

namespace Application.UseCases.V1.Clientes.AdicionarCliente
{
    public interface IAdicionarClienteUseCase
    {
        void SetOutputPort(IOutputPort outputPort);

        Task Execute(ClienteDto clienteDto);
    }
}