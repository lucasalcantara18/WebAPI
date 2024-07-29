namespace WebAPI.Modules.UseCases
{
    using Application.Services;
    using Application.UseCases.V1.Clientes.AdicionarCliente;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    ///     Adds Use Cases classes.
    /// </summary>
    public static class UseCasesExtensions
    {
        /// <summary>
        ///     Adds Use Cases to the ServiceCollection.
        /// </summary>
        /// <param name="services">Service Collection.</param>
        /// <returns>The modified instance.</returns>
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            AccountsUseCase(services);

            return services;
        }

        private static void AccountsUseCase(IServiceCollection services)
        {
            services
                .AddScoped<IAdicionarClienteUseCase, AdicionarClienteUseCase>()
                .Decorate<IAdicionarClienteUseCase, AdicionarClienteValidation>();
        }
    }
}