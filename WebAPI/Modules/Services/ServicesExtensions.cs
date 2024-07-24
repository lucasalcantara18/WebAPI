namespace WebAPI.Modules.Services
{
    using Application.Services;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    ///     Adds Use Cases classes.
    /// </summary>
    public static class ServicesExtensions
    {
        /// <summary>
        ///     Adds Use Cases to the ServiceCollection.
        /// </summary>
        /// <param name="services">Service Collection.</param>
        /// <returns>The modified instance.</returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            AddNotification(services);

            return services;
        }

        private static void AddNotification(IServiceCollection services)
        {
            services.AddScoped<Notification, Notification>();
        }
    }
}