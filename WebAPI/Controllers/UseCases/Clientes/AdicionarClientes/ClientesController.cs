using Application.Services;
using Application.UseCases.V1.Clientes.AdicionarCliente;
using Asp.Versioning;
using Domain.Dto;
using Domain.Enumerations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using WebApi.Modules.Common;

namespace WebAPI.Controllers.UseCases.Clientes.AdicionarClientes
{
    [ApiVersion("1.0")]
    [FeatureGate(CustomFeature.Clientes)]
    [Route("api/v{version:apiVersion}/[controller]/")]
    public sealed class ClientesController : ApiController, IOutputPort
    {
        public ClientesController(Notification notification, IHttpContextAccessor httpContextAccessor) : base(notification, httpContextAccessor)
        {
        }

        void IOutputPort.Invalid() =>
            _viewModel = UnprocessableEntity(_notification);

        void IOutputPort.Ok() =>
            _viewModel = Ok();

        /// <summary>
        /// get a account with the cpf
        /// </summary>
        /// <param name="useCase">use case to be used</param>
        /// <param name="cpf">cpf from the account</param>
        /// <returns>data from the account with the cpf passed</returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdicionarClientesRequest))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Post))]
        public async Task<IActionResult> ObterRegras(
            [FromServices] IAdicionarClienteUseCase useCase,
            [FromBody] AdicionarClientesRequest request)
        {
            useCase.SetOutputPort(this);

            var dto = new ClienteDto(request.Cpf, request.Usuario, request.Senha, request.Nome, request.Celular, request.Email, request.Endereco, request.Sexo);

            await useCase.Execute(dto);

            return _viewModel;
        }
    }
}