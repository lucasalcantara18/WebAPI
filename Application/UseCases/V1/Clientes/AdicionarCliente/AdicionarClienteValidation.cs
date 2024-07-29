using Application.Services;
using Domain.Dto;
using System.Threading.Tasks;

namespace Application.UseCases.V1.Clientes.AdicionarCliente
{
    public class AdicionarClienteValidation : IAdicionarClienteUseCase
    {
        private IOutputPort _outputPort;
        private readonly IAdicionarClienteUseCase _useCase;
        private readonly Notification _notification;

        public AdicionarClienteValidation(IAdicionarClienteUseCase useCase, Notification notification)
        {
            _useCase = useCase;
            _notification = notification;
            _outputPort = new CreateAccountPresenter();
        }

        public async Task Execute(ClienteDto clienteDto)
        {
            if (string.IsNullOrEmpty(clienteDto.cpf))
                _notification.Add("CPF", "Não pode ser vazio ou nulo");

            if (string.IsNullOrEmpty(clienteDto.celular))
                _notification.Add("Celular", "Tem que ser informado");

            if (string.IsNullOrEmpty(clienteDto.nome))
                _notification.Add("Nome", "Tem que ser informado");

            if (string.IsNullOrEmpty(clienteDto.email))
                _notification.Add("Email", "Tem que ser informado");

            if (_notification.IsInvalid)
            {
                _outputPort.Invalid();
                return;
            }

            await _useCase.Execute(clienteDto);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
            _useCase.SetOutputPort(outputPort);
        }
    }
}