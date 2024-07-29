using Application.Services;
using Domain.Dto;
using Domain.Enumerations;
using Domain.Interfaces.Repositories;
using Domain.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCases.V1.Clientes.AdicionarCliente
{
    public class AdicionarClienteUseCase : IAdicionarClienteUseCase
    {
        private IOutputPort _outputPort;
        private readonly IClienteRepository _clienteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AdicionarClienteUseCase(IClienteRepository clienteRepository, IUnitOfWork unitOfWork)
        {
            _clienteRepository = clienteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(ClienteDto clienteDto)
        {
            var newCliente = new Cliente(clienteDto);

            await _clienteRepository.AddAsync(newCliente);
            await _unitOfWork.SaveAsync();

            _outputPort.Ok();
        }

        public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;
    }
}