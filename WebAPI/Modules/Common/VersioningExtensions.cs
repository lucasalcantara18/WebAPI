namespace WebAPI.Modules.Common
{
    using Asp.Versioning;
    using Asp.Versioning.Conventions;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    ///     Versioning Extensions.
    /// </summary>
    public static class VersioningExtensions
    {
        /// <summary>
        ///     Method that adds versioning to the api.
        /// </summary>
        public static IServiceCollection AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(
                options =>
                {
                    // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                    options.ReportApiVersions = true;
                    options.ApiVersionReader = new UrlSegmentApiVersionReader();

                }).AddMvc(
                options =>
                {
                    // automatically applies an api version based on the name of
                    // the defining controller's namespace
                    options.Conventions.Add(new VersionByNamespaceConvention());
                })
                .AddApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            return services;
        }
    }
}