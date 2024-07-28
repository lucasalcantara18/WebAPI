using FluentMigrator.Runner;
using Infrastructure.DataAccess.Sql;
using Infrastructure.DataAccess.Sql.SqlServer;
using Microsoft.Data.SqlClient;

namespace WebApi.Modules.Database
{
    public static class MigrationExtensions
    {
        public static IServiceCollection AddMigrator(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("SqlConnectionStrings");

            return services.AddMigrator(connectionString);
        }

        public static IServiceCollection AddMigrator(this IServiceCollection services, string connectionString)
        {
            EnsureDatabaseExists(connectionString);

            return services
                .AddFluentMigratorCore()
                .ConfigureRunner(builder =>
                   builder
                       .AddSqlServer()
                       .WithVersionTable(MigrationVersionTable.Default)
                       .WithGlobalConnectionString(connectionString)
                       .ScanIn(typeof(DatabaseContext).Assembly).For.Migrations());
        }

        public static IApplicationBuilder UseDatabaseAlwaysUpToDate(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var provider = scope.ServiceProvider;
            var runner = provider.GetRequiredService<IMigrationRunner>();
            var logger = provider.GetRequiredService<ILogger<Program>>();

            logger.LogWarning("Aplicando migrações");

            runner.ListMigrations();

            runner.MigrateUp();

            return app;
        }

        private static void EnsureDatabaseExists(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            var databaseName = builder.InitialCatalog;

            builder.InitialCatalog = "master";

            using SqlConnection connection = new(builder.ConnectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = $@"
            IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{databaseName}')
            BEGIN
                CREATE DATABASE {databaseName} COLLATE SQL_Latin1_General_CP1_CI_AS;
            END;";
            command.ExecuteNonQuery();
        }
    }
}