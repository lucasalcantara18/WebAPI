using Domain.Models;

namespace Application.UseCases.V1.Clientes.AdicionarCliente
{
    public interface IOutputPort
    {
        void Ok();

        void Invalid();
    }
}