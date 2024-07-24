namespace WebAPI.Modules.Common
{
    using Asp.Versioning;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    /// <summary>
    ///     Versioning Extensions.
    /// </summary>
    public static class ContextAccessorExtensions
    {
        /// <summary>
        ///     Method that adds versioning to the api.
        /// </summary>
        public static IServiceCollection AddContextAccessor(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }
    }
}