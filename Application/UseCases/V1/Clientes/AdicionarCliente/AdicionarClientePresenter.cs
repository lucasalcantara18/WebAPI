using Domain.Models;

namespace Application.UseCases.V1.Clientes.AdicionarCliente
{
    public class CreateAccountPresenter : IOutputPort
    {
        public bool InvalidOutput { get; private set; }
        public bool IsSuccess { get; private set; }

        public void Invalid() => InvalidOutput = true;

        public void Ok() => IsSuccess = true;
    }
}