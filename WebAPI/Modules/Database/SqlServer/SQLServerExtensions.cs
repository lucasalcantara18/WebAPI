namespace WebApi.Modules.Database
{
    using Application.Services;
    using Domain.Enumerations;
    using Infrastructure.DataAccess.Sql;
    using Infrastructure.DataAccess.Sql.SqlServer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.FeatureManagement;

    /// <summary>
    ///     Persistence Extensions.
    /// </summary>
    public static class SqlServerExtensions
    {
        /// <summary>
        ///     Add Persistence dependencies varying on configuration.
        /// </summary>
        public static IServiceCollection AddSQLServer(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceProvider = services.BuildServiceProvider();
            var featureManager = serviceProvider.GetRequiredService<IFeatureManager>();
            var environment = serviceProvider.GetRequiredService<IHostEnvironment>();

            bool isEnabled = featureManager
                .IsEnabledAsync(nameof(CustomFeature.Database))
                .GetAwaiter()
                .GetResult();

            if (isEnabled)
            {
                var connectionString = configuration.GetValue<string>("SqlConnectionStrings");

                serviceProvider.GetService<ILogger<Program>>();

                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

                services.AddDbContext<DatabaseContext>(
                    options =>
                    {
                        options
                            .UseSqlServer(connectionString)
                            .UseLazyLoadingProxies()
                            .EnableSensitiveDataLogging()
                            .EnableDetailedErrors()
                            .UseLoggerFactory(loggerFactory);
                    });

                services.AddScoped<IUnitOfWork, UnitOfWork>();
            }

            return services;
        }
    }
}