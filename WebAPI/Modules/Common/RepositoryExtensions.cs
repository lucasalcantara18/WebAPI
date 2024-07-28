using Domain.Interfaces.Base;
using Infrastructure.DataAccess.Sql.Bases;

namespace WebApi.Modules.Common
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddDatabaseRepositories(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            var assembly = typeof(BaseRepository<>).Assembly;
            var types = assembly.GetExportedTypes().Where(x => x.IsClass && !x.IsAbstract && x.GetInterface(nameof(IRepositoryBase)) != null).ToList();

            types.ForEach(x =>
            {
                var interfaces = x.GetInterfaces();
                if (!interfaces.Any())
                    throw new Exception($"O repositório {x.Name}, não tem uma interface correspondente");
                var descriptor = new ServiceDescriptor(interfaces.Last(), x, serviceLifetime);
                services.Add(descriptor);
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddDatabaseRepositories();

            return services;
        }
    }
}